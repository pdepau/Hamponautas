using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Scores.Instance.current.gems++;
            AudioHolder.Instance.Play(clip);
            gameObject.SetActive(false); 
        }
    }
}
