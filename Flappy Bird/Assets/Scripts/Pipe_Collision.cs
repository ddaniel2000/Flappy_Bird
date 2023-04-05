using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Collision : MonoBehaviour
{
    public delegate void PipeCollision();
    public static event PipeCollision OnDead;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnDead?.Invoke();
        }
    }
}
