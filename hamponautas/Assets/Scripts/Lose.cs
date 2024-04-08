using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject LoseScreen;
    public GeneradorT generadorT;

    public TMPro.TextMeshProUGUI scoreText;
    private void Awake()
    {
        Obstacle.OnTrap += Obstacle_OnTrap;
    }

    private void Obstacle_OnTrap(TrapType trapType)
    {
        scoreText.text = Scores.Instance.current.ToString();
        Scores.Instance.Save();
        LoseScreen.SetActive(true);
        generadorT.enabled = false;
    }

}
