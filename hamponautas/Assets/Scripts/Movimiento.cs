using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float MaxIzq, MaxDer;
    public float Vel;
    public float jumpHeight = 3f; // Altura del salto
    public float jumpSpeed = 5f; // Velocidad del salto
    float yOriginal;
    internal Transform tr;
    Rigidbody rb;
    bool isJumping = false;

    // Define las posiciones predefinidas
    Vector3[] positions;
    int currentPositionIndex = 1; // Empezar en la posici�n del medio

    private void Awake()
    {
        tr = transform;
        yOriginal = tr.position.y;
        rb = GetComponent<Rigidbody>();

        // Inicializa las posiciones predefinidas
        positions = new Vector3[3];
        positions[0] = new Vector3(MaxIzq, yOriginal, 0); // Posici�n izquierda
        positions[1] = new Vector3(0, yOriginal, 0); // Posici�n centro
        positions[2] = new Vector3(MaxDer, yOriginal, 0); // Posici�n derecha
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

        // Si se pulsa la tecla de espacio y no estamos saltando, realizar un salto
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }

        // Mover suavemente hacia la posici�n actual
        tr.position = Vector3.MoveTowards(tr.position, positions[currentPositionIndex], Vel * Time.deltaTime);
    }

    void MoveLeft()
    {
        // Si no estamos ya en la posici�n m�s a la izquierda, mueve hacia la izquierda
        if (currentPositionIndex > 0)
        {
            currentPositionIndex--;
        }
    }

    void MoveRight()
    {
        // Si no estamos ya en la posici�n m�s a la derecha, mueve hacia la derecha
        if (currentPositionIndex < positions.Length - 1)
        {
            currentPositionIndex++;
        }
    }

    void Jump()
    {
        // Mueve la nave verticalmente hacia arriba
        StartCoroutine(MoveVertical(jumpHeight, jumpSpeed));
    }

    IEnumerator MoveVertical(float targetHeight, float speed)
    {
        isJumping = true;
        float startY = tr.position.y;
        float elapsedTime = 0f;

        // Subir
        while (elapsedTime < 1f)
        {
            float newY = Mathf.Lerp(startY, targetHeight, elapsedTime);
            tr.position = new Vector3(tr.position.x, newY, tr.position.z);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        // Bajar
        elapsedTime = 1f; // Reiniciamos el tiempo para el movimiento descendente
        while (elapsedTime > 0f)
        {
            float newY = Mathf.Lerp(startY, targetHeight, elapsedTime);
            tr.position = new Vector3(tr.position.x, newY, tr.position.z);
            elapsedTime -= Time.deltaTime * speed; // Aqu� decrementamos el tiempo
            yield return null;
        }

        tr.position = new Vector3(tr.position.x, startY, tr.position.z);
        isJumping = false;
    }
}