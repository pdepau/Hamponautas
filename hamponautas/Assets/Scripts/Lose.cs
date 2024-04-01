using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject LoseScreen;
    public GeneradorT generadorT;

    private void Awake()
    {
        Obstacle.OnTrap += Obstacle_OnTrap;
    }

    private void Obstacle_OnTrap(TrapType trapType)
    {

        LoseScreen.SetActive(true);
        generadorT.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
