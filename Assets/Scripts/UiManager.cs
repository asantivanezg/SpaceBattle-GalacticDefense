using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImagesDisplay;
    public int score;
    public Text scoreText;

    public void UpdateLives(int currentLives)
    {
        livesImagesDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }
}
