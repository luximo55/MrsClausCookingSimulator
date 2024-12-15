using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Camera main;
    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        main = Camera.main;
        if(transform.position == new Vector3(0f, -2.8f, 0.7f))
        {
            layerMask = 0;
        }
    }
    void Update()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, layerMask))
        {
            transform.position = raycasthit.point;
        }
    }
}
