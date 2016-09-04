using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public bool displayGridGizmos;
	public LayerMask unwalableMask;
	public Vector2 gridWorldSize;
	public float nodeRadious;
	public TerrainType[] walkableRegions;
	LayerMask walkableMask;
	Dictionary <int, int> walkableRegionsDictionary = new Dictionary<int, int> ();

	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake(){
		nodeDiameter = Mathf.RoundToInt(nodeRadious * 2);
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

		foreach (TerrainType region in walkableRegions) {
			walkableMask.value |= region.turrainMask.value;
			walkableRegionsDictionary.Add ((int)Mathf.Log(region.turrainMask.value, 2),region.terrainPenalty);
		}

		CreateGrid ();
	}

	public int maxSize{
		get{
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - 
			Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
		for (int x = 0; x < gridSizeX; x++){
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadious) + 
					Vector2.up * (y * nodeDiameter + nodeRadious);
				bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadious, unwalableMask));
				int movementPenalty = 0;

				if (walkable) {
					Ray ray = new Ray (worldPoint + Vector3.up * 50, Vector3.down);
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit, 100, walkableMask)){
						walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
					}
				}

				grid [x, y] = new Node (walkable, worldPoint, x, y, movementPenalty);	
			}
		}
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbors = new List<Node> ();

		for (int x = -1; x <= 1; x++){
			for (int y = -1; y <= 1; y++){
				if (x == 0 && y == 0) {
					continue;
				}
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbors.Add (grid [checkX, checkY]);
				}
			}
		}
		return neighbors;
	}

	public Node NodeFromWorldPoint(Vector2 worldPos){
		float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
		if (grid != null && displayGridGizmos) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPos, Vector2.one * (nodeDiameter - .1f));
			}
		}
	}

	[System.Serializable]
	public class TerrainType {
		public LayerMask turrainMask;
		public int terrainPenalty;
	}
}
