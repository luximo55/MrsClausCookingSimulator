using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text finalScoreText;
    public GameObject GOPanel;

    private void Start()
    {
        GOPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver(int score)
    {
        GOPanel.SetActive(true);
        Time.timeScale = 0;
        finalScoreText.text = score.ToString();
    }
}
