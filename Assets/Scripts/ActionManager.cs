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
    public int servingItems = 0;

    public GameObject cookieDough;
    public GameObject rawCookies;
    public GameObject bakedCookies;
    public GameObject cookiePlate;
    private GameObject activeObject;
    private int objectType = 0;
    private int cookieType = 0;

    private int prepareTemp = 0;
    private int ovenTemp = 0;
    private int serveTemp = 0;

    private int points = 0;

    void Update()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, layerMask))
        {
            switch (raycasthit.collider.name)
            {
                case "milkCookies":
                    if(Input.GetMouseButtonDown(0) && !activeInstantiation)
                    {
                        activeObject = Instantiate(cookieDough);
                        objectType = 1;
                        cookieType = 1;
                        activeInstantiation = true;
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 1)
                    {
                        DestroyObject();               
                    }
                    break;
                case "darkCookies":
                    if(Input.GetMouseButtonDown(0) && !activeInstantiation)
                    {
                        activeObject = Instantiate(cookieDough);
                        objectType = 1;
                        cookieType = 2;
                        activeInstantiation = true;
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 1)
                    {
                        DestroyObject();               
                    }
                    break;
                case "whiteCookies":
                    if(Input.GetMouseButtonDown(0) && !activeInstantiation)
                    {
                        activeObject = Instantiate(cookieDough);
                        objectType = 1;
                        cookieType = 3;
                        activeInstantiation = true;
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 1)
                    {
                        DestroyObject();               
                    }
                    break;
                case "prepare":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !prepareOccupied && (objectType == 1 || objectType == 2))
                    {
                        DestroyObject();
                        prepareOccupied = true;
                        prepareTemp = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && prepareOccupied)
                    {
                        activeObject = Instantiate(rawCookies);
                        objectType = 2;
                        cookieType = prepareTemp;
                        activeInstantiation = true;
                        prepareOccupied = false;
                    }
                    break;
                case "oven":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !ovenOccupied && (objectType == 2 || objectType == 3))
                    {
                        DestroyObject();
                        ovenOccupied = true;
                        ovenTemp = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && ovenOccupied)
                    {
                        activeObject = Instantiate(bakedCookies);
                        objectType = 3;
                        cookieType = ovenTemp;
                        activeInstantiation = true;
                        ovenOccupied = false;
                    }
                    break;
                case "serve":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && servingItems < 3 && (objectType == 3 || objectType == 4))
                    {
                        DestroyObject();
                        servingItems++;
                        serveTemp = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && servingItems > 0)
                    {
                        activeObject = Instantiate(cookiePlate);
                        objectType = 4;
                        cookieType = serveTemp;
                        activeInstantiation = true;
                        servingItems--;
                    }
                    break;
                case "finish":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 4)
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


