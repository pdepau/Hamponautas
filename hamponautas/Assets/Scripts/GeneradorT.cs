using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneradorT : MonoBehaviour
{
    public Pool ItemsPool;
    public Pool SafeItemsPool;
    // cola como el mercadona
    Queue<Transform> elements;

    public float Increment = 0.03f;
    public float Speed = 10f;
    public float speed;
    public int Quantity = 30;
    public int SafeQuantity = 3;
    public float Displace = 15f;
    public Vector3 direction;
    public Vector3 offset;
    Transform tr;

    Vector3 originalPos;
    float currentDisplace;
    int moved = 0;

    private void Awake()
    {
        tr = transform;
        ItemsPool.Initialize();
        SafeItemsPool.Initialize();
        elements = new Queue<Transform>();

    }

    public void Clean()
    {
        while (elements.Any())
        {
            elements.Dequeue().gameObject.SetActive(false);
        }
    }
    public void Generate()
    {
        tr.position = originalPos;
        speed = Speed;
        currentDisplace = 0;
        moved = 0;

        for (int i = 0; i < Quantity; i++)
        {
            var elementTransform = i < SafeQuantity ? SafeItemsPool.GetRandom() : ItemsPool.GetRandom();
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
        if (timesToInfinite > moved + 2)
        {
            ToInfinite();
        }
        speed += Time.deltaTime * Increment;
        Debug.Log(speed);

    }

    public void ToInfinite()
    {
        var last = elements.LastOrDefault();
        var tel = elements.Dequeue();
        tel.gameObject.SetActive(false);

        var elementTransform = ItemsPool.GetRandom();
        elementTransform.position = last.position - direction * Displace;
        elementTransform.gameObject.SetActive(true);
        var items = elementTransform.GetComponentsInChildren<Item>();
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
        }
        elements.Enqueue(elementTransform);

        Scores.Instance.current.km++;
        moved++;
    }
}