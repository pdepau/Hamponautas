using System.Collections;
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
    int currentPositionIndex = 1; // Empezar en la posición del medio

    Vector2 startTouchPosition;
    Vector2 endTouchPosition;

    void Awake()
    {
        tr = transform;
        yOriginal = tr.position.y;
        rb = GetComponent<Rigidbody>();

        // Inicializa las posiciones predefinidas
        positions = new Vector3[3];
        positions[0] = new Vector3(MaxIzq, yOriginal, 0); // Posición izquierda
        positions[1] = new Vector3(0, yOriginal, 0); // Posición centro
        positions[2] = new Vector3(MaxDer, yOriginal, 0); // Posición derecha
    }

    void Update()
    {
        // Verifica los controles táctiles si estamos en un dispositivo Android
        if (Application.platform == RuntimePlatform.Android)
        {
            CheckAndroidTouchControls();
        }
        else // Si no, usa los controles de teclado
        {
            CheckKeyboardControls();
        }

        // Mover suavemente hacia la posición actual
        tr.position = Vector3.MoveTowards(tr.position, positions[currentPositionIndex], Vel * Time.deltaTime);
    }

    void CheckAndroidTouchControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;

                    Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                    if (swipeDirection.magnitude > 50f)
                    {
                        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                        {
                            if (swipeDirection.x > 0)
                            {
                                MoveRight();
                            }
                            else
                            {
                                MoveLeft();
                            }
                        }
                        else
                        {
                            if (swipeDirection.y > 0)
                            {
                                Jump();
                            }
                        }
                    }

                    break;
            }
        }
    }

    void CheckKeyboardControls()
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
            float newY = Mathf.Lerp(targetHeight, startY, elapsedTime);
            tr.position = new Vector3(tr.position.x, newY, tr.position.z);
            elapsedTime -= Time.deltaTime * speed; // Aquí decrementamos el tiempo
            yield return null;
        }

        tr.position = new Vector3(tr.position.x, startY, tr.position.z);
        isJumping = false;
    }
}