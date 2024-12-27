using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEfficiency : MonoBehaviour
{
    [SerializeField] private RectTransform progress;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text presentsText;
    public float efficiency = 100;
    private float efficiencyMultiplier = 1;
    public float presents = 0;
    public int score = 0;

    private void Update()
    {
        //Keeps the efficiency from going above 100 or below 0
        if(efficiency > 100)
        {
            efficiency = 100f;
        }
        else if(efficiency < 0)
        {
            gameManager.GameOver(score);
            efficiency = 0f;
        }
        //Based on the efficiency it changes the size of the progress bar
        progress.localScale = new Vector3(efficiency/100, progress.localScale.y, progress.localScale.z);
        //Changes the efficiency over time based on the efficiencyMultiplier
        efficiency -= 1f * efficiencyMultiplier * Time.deltaTime;

        //Increases difficulty based on the current score, higher the score the faster it will decrease
        if(presents >= 300 && presents < 600)
            efficiencyMultiplier = 1.5f;
        else if (presents >= 600 && presents < 1000)
            efficiencyMultiplier = 2f;
        else if (presents >= 1000 && presents < 1300)
            efficiencyMultiplier = 2.5f;
        else if (presents >= 1300)
            efficiencyMultiplier = 3f;

        //Converts efficiency to presents
        presents += (5 * (efficiency/100)) * Time.deltaTime;
        score = (int)presents;
        presentsText.text = score.ToString();
    }
}
