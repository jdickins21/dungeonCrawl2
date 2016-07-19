﻿using UnityEngine;
using System.Collections;

public class Node {

	public bool walkable;
	public Vector2 worldPos;

	public Node(bool _walkable, Vector2 _worldPos){
		walkable = _walkable;
		worldPos = _worldPos;
	}
}
