using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Game");
    }

    public void SelectJet()
    {
        SceneManager.LoadScene("SelectNave");
    }

    public void Salir()
    {
        Debug.Log("saliendo");
        Application.Quit();
    }
}
