using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaManager : MonoBehaviour
{
    [SerializeField] ActionManager actionManager;

    private float intervalTime;

    private void Start()
    {
        StartInterval();
    }

    private void StartInterval()
    {
        intervalTime = Random.Range(30, 100);
        Debug.Log("Santa will intialize in " + intervalTime);
        Invoke("IntializeSanta", intervalTime);
    }

    private void InitializeSanta()
    {
        Debug.Log("Santa is initialized");
    }

}
