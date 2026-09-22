using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class InputManager_ : MonoBehaviour
{
    [SerializeField]
    private Camera scenceCamera;

    [SerializeField]
    private LayerMask placementLayermask;

    private Vector3 lastpositon;

    public GameObject Gohit;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = scenceCamera.nearClipPlane;
        Ray ray = scenceCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            Gohit = hit.transform.gameObject;
            float gridSize = 2f;
            float snappedX = Mathf.Round(hit.point.x / gridSize) * gridSize;
            float snappedZ = Mathf.Round(hit.point.z / gridSize) * gridSize;
            lastpositon = new Vector3(snappedX, hit.point.y, snappedZ);
        }

        return lastpositon;    
    }
}
