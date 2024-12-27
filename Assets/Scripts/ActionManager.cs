using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Transactions;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private Camera main;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private CustomerController customerController;

    private bool activeInstantiation;
    private bool prepareOccupied = false;
    private bool ovenOccupied = false;
    private bool ovenActive = false;
    private bool preparing = false;
    private bool serving1 = false;
    private bool serving2 = false;
    public bool servingOccupied1 = false;
    public bool servingOccupied2 = false;

    [Header("Coordinates")]
    public Vector3[] trayPositions;
    public Vector3[] platePositions1;
    public Vector3[] platePositions2;
    public Vector3[] plateRotations;
    public Vector3 ovenTrayPosition;
    [Header("Prefabs")]
    public GameObject[] prepareCookies;
    public GameObject[] serveCookies;
    public GameObject[] cookieDough;
    public GameObject[] rawCookies;
    public GameObject[] bakedCookies;
    public GameObject[] cookiePlate;
    public Animator ovenAnim;
    private GameObject activeOvenTray;
    private GameObject activeObject;
    private GameObject[] activePrepareCookies = new GameObject[4];
    private GameObject[] activeServeCookies1 = new GameObject [4];
    private GameObject[] activeServeCookies2 = new GameObject [4];
    private int objectType = 0;
    private int cookieType = 0;
    public int cookieTypeServed = 0;

    private int prepareTemp = 0;
    private int ovenTemp = 0;
    private int serveTemp1 = 0;
    private int serveTemp2 = 0;
    
    void Update()
    {
        //Used to detect where exactly in the world has the ray been casted
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        //Detects what object the ray hit
        if(Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, layerMask))
        {
            switch (raycasthit.collider.name)
            {
                /*When the ray hits an object called xCookies it sets variables such as objectType and cookieType 
                so that it can transfer those to later functions

                If there is already something that "the player is holding" and has objectType 1 (raw dough), 
                then it just removes that object
                */
                case "milkCookies":
                    if(Input.GetMouseButtonDown(0) && !activeInstantiation)
                    {
                        activeObject = Instantiate(cookieDough[0]);
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
                        activeObject = Instantiate(cookieDough[1]);
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
                        activeObject = Instantiate(cookieDough[2]);
                        objectType = 1;
                        cookieType = 3;
                        activeInstantiation = true;
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 1)
                    {
                        DestroyObject();               
                    }
                    break;
                /*Most of these stations check the same thing, that being:
                    - setting a timer for z seconds
                    - activating animations
                    - destroying the object
                    - preserving the type of cookie
                Essentially they are used to make actions the pretty much seamless
                Also that but in reverse
                */
                case "prepare":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !prepareOccupied && (objectType == 1 || objectType == 2))
                    {
                        if(objectType == 1)
                        {
                            Invoke("PrepareTimer", 2f);
                            StartCoroutine(PrepareAnimation(cookieType));
                            preparing = true;
                        }
                        else if (objectType == 2)
                        {
                            //This for does the same thing the for inside of "PrepareAnimation" and "ServeAnimation" does with cookies but instantly 
                            for(int i = 0; i < 4; i++)
                            {
                                activePrepareCookies[i] = Instantiate(prepareCookies[cookieType-1], trayPositions[i], Quaternion.Euler(0,0,0));
                            }
                        }
                        DestroyObject();
                        prepareOccupied = true;
                        prepareTemp = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && prepareOccupied && !preparing)
                    {
                        activeObject = Instantiate(rawCookies[prepareTemp-1]);
                        objectType = 2;
                        cookieType = prepareTemp;
                        activeInstantiation = true;
                        prepareOccupied = false;
                        for(int i = 0; i < 4; i++)
                        {
                            Destroy(activePrepareCookies[i]);
                        }
                    }
                    break;
                case "oven":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !ovenOccupied && (objectType == 2 || objectType == 3))
                    {
                        if(objectType == 2)
                        {
                            ovenAnim.SetTrigger("Toggle");
                            StartCoroutine(OvenBake(cookieType));
                            activeOvenTray = Instantiate(rawCookies[cookieType-1], ovenTrayPosition, Quaternion.identity);
                            ovenActive = true;
                        }
                        else if (objectType == 3)
                        {
                            
                        }
                        DestroyObject();
                        ovenOccupied = true;
                        ovenTemp = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && ovenOccupied && !ovenActive)
                    {
                        activeObject = Instantiate(bakedCookies[ovenTemp-1]);
                        Destroy(activeOvenTray);
                        objectType = 3;
                        cookieType = ovenTemp;
                        activeInstantiation = true;
                        ovenOccupied = false;
                    }
                    break;
                case "serve1":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !servingOccupied1 && (objectType == 3 || objectType == 4))
                    {
                        if(objectType == 3)
                        {
                            StartCoroutine(ServeTimer(1));
                            StartCoroutine(ServeAnimation(cookieType, 1));
                            serving1 = true;
                        }
                        else if (objectType == 4)
                        {
                            for(int i = 0; i < 4; i++)
                            {
                                activeServeCookies1[i] = Instantiate(serveCookies[cookieType-1], platePositions1[i], Quaternion.Euler(plateRotations[i]));
                            }
                        }
                        DestroyObject();
                        servingOccupied1 = true;
                        serveTemp1 = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && servingOccupied1 && !serving1)
                    {
                        activeObject = Instantiate(cookiePlate[serveTemp1-1]);
                        objectType = 4;
                        cookieType = serveTemp1;
                        activeInstantiation = true;
                        servingOccupied1 = false;
                        for(int i = 0; i < 4; i++)
                        {
                            Destroy(activeServeCookies1[i]);
                        }
                    }
                    break;
                case "serve2":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && !servingOccupied2 && (objectType == 3 || objectType == 4))
                    {
                        if(objectType == 3)
                        {
                            StartCoroutine(ServeTimer(2));
                            StartCoroutine(ServeAnimation(cookieType, 2));
                            serving2 = true;
                        }
                        else if (objectType == 4)
                        {
                            for(int i = 0; i < 4; i++)
                            {
                                activeServeCookies2[i] = Instantiate(serveCookies[cookieType-1], platePositions2[i], Quaternion.Euler(plateRotations[i]));
                            }
                        }
                        DestroyObject();
                        servingOccupied2 = true;
                        serveTemp2 = cookieType;
                    }
                    else if (Input.GetMouseButtonDown(0) && !activeInstantiation && !activeObject && servingOccupied2 && !serving2)
                    {
                        activeObject = Instantiate(cookiePlate[serveTemp2-1]);
                        objectType = 4;
                        cookieType = serveTemp2;
                        activeInstantiation = true;
                        servingOccupied2 = false;
                        for(int i = 0; i < 4; i++)
                        {
                            Destroy(activeServeCookies2[i]);
                        }
                    }
                    break;
                case "finish1":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 4 && customerController.customerActive1)
                    {
                        DestroyObject();
                        SendOrder();
                        customerController.CheckOrder(cookieTypeServed, 1);
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType != 4 && customerController.customerActive1)
                    {
                        DestroyObject();
                        customerController.RawOrder(1);
                    }
                    break;
                case "finish2":
                    if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType == 4 && customerController.customerActive2)
                    {
                        DestroyObject();
                        SendOrder();
                        customerController.CheckOrder(cookieTypeServed, 2);
                    }
                    else if(Input.GetMouseButtonDown(0) && activeInstantiation && objectType != 4 && customerController.customerActive2)
                    {
                        DestroyObject();
                        customerController.RawOrder(2);
                    }
                    break;
            }
        }
    }

    private IEnumerator PrepareAnimation(int type)
    {   
        for(int i = 0; i < 4; i++)
        {
            activePrepareCookies[i] = Instantiate(prepareCookies[type-1], trayPositions[i], Quaternion.Euler(0,0,0));
            activePrepareCookies[i].GetComponent<Animator>().SetTrigger("NewDough");
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ServeAnimation(int type, int servingStation)
    {
        switch(servingStation)
        {
            case 1:
                for(int i = 0; i < 4; i++)
                {
                        activeServeCookies1[i] = Instantiate(serveCookies[type-1], platePositions1[i], Quaternion.Euler(plateRotations[i]));
                        activeServeCookies1[i].GetComponent<Animator>().SetTrigger("NewDough");
                        yield return new WaitForSeconds(0.25f);
                }
                break;
            case 2:
                for(int i = 0; i < 4; i++)
                {
                        activeServeCookies2[i] = Instantiate(serveCookies[type-1], platePositions2[i], Quaternion.Euler(plateRotations[i]));
                        activeServeCookies2[i].GetComponent<Animator>().SetTrigger("NewDough");
                        yield return new WaitForSeconds(0.25f);
                }
                break;
        }
    }

    //yTimer (and OvenBake) is used to "end" the action made on the cookies (for example, baking, when the timer is done, the baking is done)
    private IEnumerator ServeTimer(int num)
    {
        yield return new WaitForSeconds(1.1f);
        switch (num)
        {
            case 1:
                serving1 = false;
                break;
            case 2:
                serving2 = false;
                break;
        }
    }

    private void PrepareTimer()
    {
        preparing = false;
    }

    //Along with the timer function, it has to manage how it moves and what happens with the tray going in
    private IEnumerator OvenBake(int cookieType)
    {
        yield return new WaitForSeconds(3f);
        Destroy(activeOvenTray);
        activeOvenTray = Instantiate(bakedCookies[cookieType-1], ovenTrayPosition, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        ovenActive = false;
    }

    //Destroys the activeObject, resets temporary variables
    private void DestroyObject()
    {
        Destroy(activeObject);
        objectType = 0;
        activeInstantiation = false;
    }

    /*When the order is given to the elf, it sets the cookieTypeServed as the type of cookie given to them
    It it later read by another function that checks the validity of the order
    */
    private void SendOrder()
    {
        cookieTypeServed = cookieType;
    }

    //check which serving Santa has chosen to steal from, and removes them
    public void SantaStealCookies(int servePlace)
    {
        if(servePlace == 1)
        {
            serveTemp1 = 0;
            servingOccupied1 = false;
            for(int i = 0; i < 4; i++)
            {
                Destroy(activeServeCookies1[i]);
            }
        }
        else if(servePlace == 2)
        {
            serveTemp2 = 0;
            servingOccupied2 = false;
            for(int i = 0; i < 4; i++)
            {
                Destroy(activeServeCookies2[i]);
            }
        }
    }
}


