using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enviroment_Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float _lifetime = 20;

    public delegate void Trigger();
    public static event Trigger OnTrigger;

    private float time;
    

    private void OnEnable()
    {
        Game_Manager.OnTime += Timer;
        Game_Manager.OnSpeedGain += MoveSpeed;
    }
    private void OnDisable()
    {
        Game_Manager.OnTime -= Timer;
        Game_Manager.OnSpeedGain -= MoveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        _lifetime -= time;
        if (_lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
            transform.Translate(moveSpeed * time, 0, 0);

        moveSpeed += 0.5f;
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTrigger?.Invoke();
        }
    }

    private void Timer(float _time)
    {
        //Debug.Log(time);
        time = _time;

    }

    private void MoveSpeed(float _movespeed)
    {
        moveSpeed = _movespeed;
    }
}   
