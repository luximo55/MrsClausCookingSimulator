using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Transactions;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private Camera main;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private CustomerController customerController;
    [SerializeField] private StationManager stationManager;

    private bool activeInstantiation;
    private bool prepareOccupied = false;
    private bool ovenOccupied = false;
    private bool ovenActive = false;
    private bool preparing = false;
    public bool servingOccupied1 = false;
    public bool servingOccupied2 = false;

    public GameObject cookieDough;
    public GameObject rawCookies;
    public GameObject bakedCookies;
    public GameObject cookiePlate;
    private GameObject activeObject;
    private int objectType = 0;
    private int cookieType = 0;
    public int cookieTypeServed = 0;

    private int prepareTemp = 0;
    private int ovenTemp = 0;
    private int serveTemp1 = 0;
    private int serveTemp2 = 0;

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
                        if(objectType == 1)
                        {
                            Invoke("PrepareTimer", 2f);
                            preparing = true;
                        }
                        DestroyObject();
                        prepareOccupied = true;
                        prepareTemp = cookieType;
                        stationManager.ChangeMaterial("prepare", prepareOccupied);
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && prepareOccupied && !preparing)
                    {
                        activeObject = Instantiate(rawCookies);
                        objectType = 2;
                        cookieType = prepareTemp;
                        activeInstantiation = true;
                        prepareOccupied = false;
                        stationManager.ChangeMaterial("prepare", prepareOccupied);
                    }
                    break;
                case "oven":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !ovenOccupied && (objectType == 2 || objectType == 3))
                    {
                        if(objectType == 2)
                        {
                            Invoke("OvenTimer", 5f);
                            ovenActive = true;
                        }
                        DestroyObject();
                        ovenOccupied = true;
                        ovenTemp = cookieType;
                        stationManager.ChangeMaterial("oven", ovenOccupied);
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && ovenOccupied && !ovenActive)
                    {
                        activeObject = Instantiate(bakedCookies);
                        objectType = 3;
                        cookieType = ovenTemp;
                        activeInstantiation = true;
                        ovenOccupied = false;
                        stationManager.ChangeMaterial("oven", ovenOccupied);
                    }
                    break;
                case "serve1":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !servingOccupied1 && (objectType == 3 || objectType == 4))
                    {
                        DestroyObject();
                        servingOccupied1 = true;
                        serveTemp1 = cookieType;
                        stationManager.ChangeMaterial("serve1", servingOccupied1);
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && servingOccupied1)
                    {
                        activeObject = Instantiate(cookiePlate);
                        objectType = 4;
                        cookieType = serveTemp1;
                        activeInstantiation = true;
                        servingOccupied1 = false;
                        stationManager.ChangeMaterial("serve1", servingOccupied1);
                    }
                    break;
                case "serve2":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !servingOccupied2 && (objectType == 3 || objectType == 4))
                    {
                        DestroyObject();
                        servingOccupied2 = true;
                        serveTemp2 = cookieType;
                        stationManager.ChangeMaterial("serve2", servingOccupied2);
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && servingOccupied2)
                    {
                        activeObject = Instantiate(cookiePlate);
                        objectType = 4;
                        cookieType = serveTemp2;
                        activeInstantiation = true;
                        servingOccupied2 = false;
                        stationManager.ChangeMaterial("serve2", servingOccupied2);
                    }
                    break;
                case "finish":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 4 && customerController.customerActive)
                    {
                        DestroyObject();
                        SendOrder();
                        customerController.CheckOrder(cookieTypeServed);
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType != 4 && customerController.customerActive)
                    {
                        DestroyObject();
                        customerController.RawOrder();
                    }
                    break;
            }
        }
    }

    private void PrepareTimer()
    {
        preparing = false;
        Debug.Log("Preparing over");
    }
    private void OvenTimer()
    {
        ovenActive = false;
        Debug.Log("Baking Over");
    }
    private void DestroyObject()
    {
        Destroy(activeObject);
        objectType = 0;
        activeInstantiation = false;
    }
    private void SendOrder()
    {
        cookieTypeServed = cookieType;
    }
}


