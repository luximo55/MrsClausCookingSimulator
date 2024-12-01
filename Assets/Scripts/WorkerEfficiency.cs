using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerEfficiency : MonoBehaviour
{
    [SerializeField] private RectTransform progress;
    public float Efficiency = 100;

    private void Update()
    {
        if(Efficiency > 100)
        {
            Efficiency = 100f;
        }
        else if(Efficiency < 0)
        {
            Debug.Log("Game over");
            Efficiency = 0f;
        }
        progress.localScale = new Vector3(Efficiency/100, progress.localScale.y, progress.localScale.z);
        Efficiency -= 1.5f * Time.deltaTime;
    }
}
