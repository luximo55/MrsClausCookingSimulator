using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField] private GameObject prepare;
    [SerializeField] private GameObject oven;
    [SerializeField] private GameObject serve;

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
            case "serve":
                if(occupied)
                {
                    serve.GetComponent<MeshRenderer>().material = occupiedMat;
                }
                else if (!occupied)
                {
                    serve.GetComponent<MeshRenderer>().material = unoccupiedMat;
                }
                break;
        }
    }
}
