using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem_ : MonoBehaviour
{
    [SerializeField]
    private InputManager_ inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private GameObject mouseIndicator,cellIndicator;

    [SerializeField]
    private CameraSwitch cs;

    private void Update()
    {
        
        if (cs.capsLock == false)
        {

            cellIndicator.SetActive(true);
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = grid.CellToLocal(gridPosition);

        }
        else
        {
            cellIndicator.SetActive(false);
        }
    }
}
