using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEfficiency : MonoBehaviour
{
    [SerializeField] private RectTransform progress;
    [SerializeField] private Text presentsText;
    public float Efficiency = 100;
    public float presents = 0;
    public int score = 0;

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

        presents += (5 * (Efficiency/100)) * Time.deltaTime;
        score = (int)presents;
        presentsText.text = score.ToString();
    }
}
