using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaManager : MonoBehaviour
{
    [SerializeField] ActionManager actionManager;
    [SerializeField] GameObject santa;

    private Transform transf;
    private GameObject activeSanta;
    private float intervalTime;

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
        if(activeSanta != null && activeSanta.GetComponent<Transform>().localPosition.x == 17)
        {
            Destroy(activeSanta);
        }
    }

    private void StartInterval()
    {
        intervalTime = 5;//Random.Range(30, 100);
        Debug.Log("Santa will intialize in " + intervalTime);
        Invoke("InitializeSanta", intervalTime);
    }

    private void InitializeSanta()
    {
        Debug.Log("Santa is initialized");
        activeSanta = Instantiate(santa, transf);
    }
}
