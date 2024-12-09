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
        switch (stationName)
        {
            case "prepare":
                if(occupied)
                {
                    prepare.GetComponent<MeshRenderer>().material = occupiedMat;
                }
                else if (!occupied)
                {
                    prepare.GetComponent<MeshRenderer>().material = unoccupiedMat;
                }
                break;
            case "oven":
                if(occupied)
                {
                    oven.GetComponent<MeshRenderer>().material = occupiedMat;
                }
                else if (!occupied)
                {
                    oven.GetComponent<MeshRenderer>().material = unoccupiedMat;
                }
                break;
            case "serve1":
                if(occupied)
                {
                    serve1.GetComponent<MeshRenderer>().material = occupiedMat;
                }
                else if (!occupied)
                {
                    serve1.GetComponent<MeshRenderer>().material = unoccupiedMat;
                }
                break;
            case "serve2":
                if(occupied)
                {
                    serve2.GetComponent<MeshRenderer>().material = occupiedMat;
                }
                else if (!occupied)
                {
                    serve2.GetComponent<MeshRenderer>().material = unoccupiedMat;
                }
                break;
        }
    }
}
