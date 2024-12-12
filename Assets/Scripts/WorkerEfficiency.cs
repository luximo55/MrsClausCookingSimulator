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
            gameManager.GameOver(score);
            Efficiency = 0f;
        }
        progress.localScale = new Vector3(Efficiency/100, progress.localScale.y, progress.localScale.z);
        Efficiency -= 1f * Time.deltaTime;
        //TODO: increasing difficulty with score, check score in Update and after (let's say) 100 it increases by .5
        presents += (5 * (Efficiency/100)) * Time.deltaTime;
        score = (int)presents;
        presentsText.text = score.ToString();
    }
}
