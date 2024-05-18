using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        index = PlayerPrefs.GetInt("JugadorIndex");
        if (index > gameManager.jets.Count - 1)
        {
            index = 0;
        }
        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite = gameManager.jets[index].imagen;
        nombre.text = gameManager.jets[index].nombre;
    }
    public void SiguientePersonaje()
    {
        if (index == gameManager.jets.Count - 1) 
        {
            index = 0;
        }
        else 
        {
            index += 1;
        }
        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        if (index == 0)
        {
            index = gameManager.jets.Count - 1;
        }
        else
        {
            index -= 1;
        }
        CambiarPantalla();
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}

