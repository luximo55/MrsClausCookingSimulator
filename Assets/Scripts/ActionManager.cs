using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private Camera main;
    [SerializeField] private LayerMask layerMask;

    private bool activeInstantiation;
    private bool prepareOccupied = false;
    private bool ovenOccupied = false;
    public GameObject cookieDough;
    public GameObject bakedCookies;
    private GameObject activeObject;
    private int objectType = 0;

    private int points = 0;

    void Update()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, layerMask))
        {
            switch (raycasthit.collider.name)
            {
                case "start":
                    if(Input.GetMouseButtonDown(0) && !activeInstantiation)
                    {
                        activeObject = Instantiate(cookieDough);
                        objectType = 1;
                        activeInstantiation = true;
                    }
                    else if(Input.GetMouseButtonDown(1) && activeInstantiation && objectType == 1)
                    {
                        DestroyObject();               
                    }
                    break;

                //TODO: make separate prefab for non-baked cookies and assign it to the variable
                case "prepare":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !prepareOccupied && objectType == 1)
                    {
                        DestroyObject();
                        prepareOccupied = true;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && prepareOccupied)
                    {
                        activeObject = Instantiate(bakedCookies);
                        objectType = 2;
                        activeInstantiation = true;
                        prepareOccupied = false;
                    }
                    break;
                case "oven":
                    
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !ovenOccupied && objectType == 2)
                    {
                        DestroyObject();
                        ovenOccupied = true;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && ovenOccupied)
                    {
                        activeObject = Instantiate(bakedCookies);
                        objectType = 3;
                        activeInstantiation = true;
                        ovenOccupied = false;
                    }
                    break;
                case "finish":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 3)
                    {
                        DestroyObject();
                        Points();
                    }
                    break;
            }
        }
    }
    private void DestroyObject()
    {
        Destroy(activeObject);
        objectType = 0;
        activeInstantiation = false;
    }
    private void Points()
    {
        points++;
    }
}


