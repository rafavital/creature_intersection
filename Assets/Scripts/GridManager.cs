using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float _gridSize = 1;
    private Vector3[] _grid;
    public Vector3 GetPointOnGrid (Vector3 v) 
    {
        v.x = v.x - v.x % _gridSize;
        v.y = v.y - v.y % _gridSize;
        v.z = v.z - v.z % _gridSize;
        return v;
    }
}
