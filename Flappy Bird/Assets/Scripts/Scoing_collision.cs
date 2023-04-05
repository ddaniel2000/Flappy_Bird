using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoing_collision : MonoBehaviour
{
    public delegate void ScoringCollision();
    public static event ScoringCollision OnScore;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            OnScore?.Invoke();
        }
    }
}
