using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoJet", menuName ="Jet")]
public class SelectJet : ScriptableObject
{
    public GameObject jetJugable;
    public Sprite imagen;
    public string nombre;
}
