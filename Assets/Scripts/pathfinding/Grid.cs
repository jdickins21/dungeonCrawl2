﻿using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public LayerMask unwalableMask;
	public Vector2 gridWorldSize;
	public float nodeRadious;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start(){
		nodeDiameter = Mathf.RoundToInt(nodeRadious * 2);
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - 
			Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
		for (int x = 0; x < gridSizeX; x++){
			for (int y = 0; y < gridSizeY; y++) {
				Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadious) + 
					Vector2.up * (y * nodeDiameter + nodeRadious);
				bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadious, unwalableMask));
				grid [x, y] = new Node (walkable, worldPoint);	
			}
		}
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
		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPos, Vector2.one * (nodeDiameter - .1f));
			}
		}
	}
}
