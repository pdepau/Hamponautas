using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneradorT : MonoBehaviour
{
    public Pool ItemsPool;
    // cola como el mercadona
    Queue<Transform> elements;

    public float Increment = 0.03f;
    public float speed;
    public int Quantity = 30;
    public float Displace = 15f;
    public Vector3 direction;
    public Vector3 offset;
    Transform tr;

    Vector3 originalPos;
    float currentDisplace;
    int moved = 0;
    private void OnEnable()
    {
        ItemsPool.Initialize();
        tr = transform;
        elements = new Queue<Transform>();
        for (int i = 0; i < Quantity; i++)
        {
           var elementTransform= ItemsPool.GetRandom();
            elementTransform.position = offset - direction * Displace * i;
            elementTransform.gameObject.SetActive(true);
            elements.Enqueue(elementTransform);
        }
    }
    private void Update()
    {
        tr.position += direction * speed * Time.deltaTime;

        currentDisplace = Mathf.Abs(Vector3.Distance(tr.position, originalPos));
        var timesToInfinite = currentDisplace / Displace;
        if (timesToInfinite > moved +2) {
            ToInfinite();
        }
        if (speed<15) {
            speed += Time.deltaTime * Increment;
        }

        if (speed > 15)
        {
            speed -= Time.deltaTime * Increment;
        }

    }

    public void ToInfinite()
    {
        var last = elements.LastOrDefault();
        var tel = elements.Dequeue();
        tel.gameObject.SetActive(false);

        var elementTransform = ItemsPool.GetRandom();
        elementTransform.position = last.position - direction * Displace;
        elementTransform.gameObject.SetActive(true);
        elements.Enqueue(elementTransform);

        moved++;
    }
}
