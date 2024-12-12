using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
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
        if(efficiency > 100)
        {
            efficiency = 100f;
        }
        else if(efficiency < 0)
        {
            gameManager.GameOver(score);
            efficiency = 0f;
        }
        progress.localScale = new Vector3(efficiency/100, progress.localScale.y, progress.localScale.z);
        efficiency -= 1f * efficiencyMultiplier * Time.deltaTime;
        //TODO: increasing difficulty with score, check score in Update and after (let's say) 100 it increases by .5
        if(presents >= 100 && presents < 300){
            efficiencyMultiplier = 1.5f;
            Debug.Log("LEVEL UP! LEVEL: 2");}
        else if (presents >= 300&& presents < 500){
            efficiencyMultiplier = 2f;
            Debug.Log("LEVEL UP! LEVEL: 3");}
        else if (presents >= 500 && presents < 700){
            efficiencyMultiplier = 2.5f;
            Debug.Log("LEVEL UP! LEVEL: 4");}
        else if (presents >= 700){
            efficiencyMultiplier = 3f;
            Debug.Log("LEVEL UP! LEVEL: 5");}
        presents += (5 * (efficiency/100)) * Time.deltaTime;
        score = (int)presents;
        presentsText.text = score.ToString();
    }
}
