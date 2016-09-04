using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour {

	PathRequestManager requestManager;

	Grid grid;

	void Awake(){
		requestManager = GetComponent<PathRequestManager> ();
		grid = GetComponent<Grid> ();
	}

	public void startFindPath(Vector3 startPos, Vector3 targetPos){
		StartCoroutine (FindPath (startPos, targetPos));
	}

	//Sets the start and target node and finds a cheep path
	IEnumerator FindPath(Vector3 start, Vector3 target){
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.NodeFromWorldPoint (start);
		Node targetNode = grid.NodeFromWorldPoint (target);

		if (startNode.walkable && targetNode.walkable) {

			Heap<Node> openSet = new Heap<Node> (grid.maxSize);
			HashSet<Node> closedSet = new HashSet<Node> ();

			openSet.Add (startNode);
			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add (currentNode);
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains (neighbour)) {
						continue;
					}
					int newMovementCostToNeighbor = currentNode.gCost + GetDistance (currentNode, neighbour) + neighbour.movementPenalty;
					if (newMovementCostToNeighbor < neighbour.gCost || !openSet.Contains (neighbour)) {
						neighbour.gCost = newMovementCostToNeighbor;
						neighbour.hCost = GetDistance (neighbour, targetNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains (neighbour)) {
							openSet.Add (neighbour);
						} else {
							openSet.UpdateItem (neighbour);
						}
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = retracePath (startNode, targetNode);
		}
		requestManager.finishedProcessingPath (waypoints, pathSuccess);
	}

	Vector3[] retracePath(Node start, Node end){
		List<Node> path = new List<Node> ();
		Node curNode = end;

		while (curNode != start) {
			path.Add (curNode);
			curNode = curNode.parent;
		}
		Vector3[] waypoints = SimplifyPath (path);
		Array.Reverse (waypoints);
		return waypoints;
	}

	Vector3[] SimplifyPath(List<Node> path){
		List<Vector3> waypoints = new List<Vector3> ();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2 (path [i - 1].gridX - path [1].gridX, path [i - 1].gridY - path [1].gridY);
			if (directionNew != directionOld) {
				waypoints.Add (path [i].worldPos);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray ();
	}

	int GetDistance(Node nodeA, Node nodeB){
		int distX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int distY = Mathf.Abs (nodeA.gridY - nodeB.gridY);
		if (distX < distY) {
			return 14 * distY + 10 * (distX - distY);
		}
		return 14 * distX + 10 * (distY - distX);
	}
}
