using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform _objectPlacePos;
    [SerializeField] private GridManager gridManager;

    private void Update() {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Vector3 objectPos = gridManager.GetPointOnGrid(_objectPlacePos.position);
        Instantiate(_objectPrefab, objectPos, Quaternion.identity);
    }
}
