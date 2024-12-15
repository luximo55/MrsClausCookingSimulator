using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField] private GameObject prepare;
    [SerializeField] private GameObject oven;
    [SerializeField] private GameObject serve1;
    [SerializeField] private GameObject serve2;

    public Material occupiedMat;
    public Material unoccupiedMat;

    public void ChangeMaterial(string stationName, bool occupied)
    {
    }
}
