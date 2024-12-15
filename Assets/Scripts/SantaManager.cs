using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaManager : MonoBehaviour
{
    [SerializeField] ActionManager actionManager;
    [SerializeField] GameObject santaSleigh;
    [SerializeField] GameObject santa;

    private Transform transf;
    private GameObject activeSanta;
    private float intervalTime;
    private int state = 0;
    private Vector3 santaPos = new Vector3(-8, 2, 6);

    private void Awake()
    {
        transf = GetComponent<Transform>();
    }
    private void Start()
    {
        StartInterval();
    }

    private void Update()
    {
        if(activeSanta != null && activeSanta.GetComponent<Transform>().localPosition.x == 70 && state == 1)
        {
            Destroy(activeSanta);
            Invoke("SantaSteal", 5f);
        }
        else if (activeSanta != null && activeSanta.GetComponent<Transform>().localPosition.x == -9 && state == 2)
        {
            Destroy(activeSanta);
        }
    }

    private void StartInterval()
    {
        intervalTime = Random.Range(20, 60);
        Debug.Log("Santa will intialize in " + intervalTime);
        Invoke("InitializeSanta", intervalTime);
    }

    private void InitializeSanta()
    {
        Debug.Log("Santa is initialized");
        state = 1;
        activeSanta = Instantiate(santaSleigh, transf);
        
    }

    private void SantaSteal()
    {
        state = 2;
        activeSanta = Instantiate(santa, santaPos, Quaternion.Euler(0, 90, 0));
        Invoke("RemoveCookies", 2.5f);
        Invoke("StartInterval", 5f);
    }

    public void RemoveCookies()
    {
        if(actionManager.servingOccupied1)
        {
            actionManager.SantaStealCookies(1);
        }
        else if(actionManager.servingOccupied2)
        {
            actionManager.SantaStealCookies(2);
        }
    }
}
