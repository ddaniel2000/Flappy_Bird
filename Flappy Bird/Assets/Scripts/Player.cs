using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Vector3 _direction;

    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float strenght = 4f;

    public delegate void PlayerJumo();
    public static event PlayerJumo OnJump;

    private Vector3 _respawnPosition;

    private float time;
    private void OnEnable()
    {
        Game_Manager.OnTime += Timer;
        Pipe_Collision.OnDead += RestartGame;
    }
    private void OnDisable()
    {
        Game_Manager.OnTime += Timer;
        Pipe_Collision.OnDead += RestartGame;
    }
    private void Start()
    {
        _respawnPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
        
        //input for Windows / Webgl
        //Press space or left click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _direction = Vector3.up * strenght;
            OnJump?.Invoke();

        }

        //input for Android
        //count how many fingers 
        if(Input.touchCount > 0)
        {
            //if is more than 0, get that touch
            Touch touch = Input.GetTouch(0);

            //check the state of the touch (ex: began, ended, etc)
            if(touch.phase == TouchPhase.Began)
            {
                _direction = Vector3.up * strenght;
            }
        }

        //Go down with gravity on y axis
        _direction.y += gravity * time;

        //Update the position of the bird
        transform.position += _direction * time;  //Time.deltaTime it makes the calclation framerate independent and consistent


    }


    private void Timer(float _time)
    {
        //Debug.Log(time);
        time = _time;
    }
    private void RestartGame()
    {
        transform.position = _respawnPosition;
    }
}
