using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float MaxIzq, MaxDer;
    public float Vel;
    float yOriginal;
    internal Transform tr;

    // Define las posiciones predefinidas
    Vector3[] positions;
    int currentPositionIndex = 1; // Empezar en la posición del medio

    private void Awake()
    {
        tr = transform;
        yOriginal = tr.position.y;

        // Inicializa las posiciones predefinidas
        positions = new Vector3[3];
        positions[0] = new Vector3(MaxIzq, yOriginal, 0); // Posición izquierda
        positions[1] = new Vector3(0, yOriginal, 0); // Posición centro
        positions[2] = new Vector3(MaxDer, yOriginal, 0); // Posición derecha
    }

    void Update()
    {
        // Si se pulsa A o Flecha Izquierda, mover hacia la izquierda
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        // Si se pulsa D o Flecha Derecha, mover hacia la derecha
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // Mover suavemente hacia la posición actual
        tr.position = Vector3.MoveTowards(tr.position, positions[currentPositionIndex], Vel * Time.deltaTime);
    }

    void MoveLeft()
    {
        // Si no estamos ya en la posición más a la izquierda, mueve hacia la izquierda
        if (currentPositionIndex > 0)
        {
            currentPositionIndex--;
        }
    }

    void MoveRight()
    {
        // Si no estamos ya en la posición más a la derecha, mueve hacia la derecha
        if (currentPositionIndex < positions.Length - 1)
        {
            currentPositionIndex++;
        }
    }
}