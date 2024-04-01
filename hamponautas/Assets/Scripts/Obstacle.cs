using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    static public event TrapEvent OnTrap;

    public TrapType TrapType = TrapType.obstacle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnTrap?.Invoke(TrapType);
        }

    }

    private void OnDestroy()
    {
        OnTrap = null;
    }
}

public delegate void TrapEvent(TrapType trapType);

public enum TrapType
{
    obstacle,cloud,meteor,satelite
}
