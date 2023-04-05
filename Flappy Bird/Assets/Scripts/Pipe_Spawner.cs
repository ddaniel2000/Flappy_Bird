using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;
 
    [SerializeField]
    private float spawnRate = 1f;
    [SerializeField]
    private float minHeight = -1f;
    [SerializeField]
    private float maxHeight = 1f;

    private bool check = false;
    private float time = 1;
    private float moveSpeed;
    private int i;
    private void OnEnable()
    {
        Game_Manager.OnTime += Timer;
        Game_Manager.OnSpeedGain += OnSpeedChanged;
        Pipe_Collision.OnDead += RestartGame;


    }
    private void OnDisable()
    {

        CancelInvoke(nameof(Spawn));
        Game_Manager.OnTime -= Timer;
        Game_Manager.OnSpeedGain -= OnSpeedChanged;
        Pipe_Collision.OnDead -= RestartGame;
    }

    private void Update()
    {

        
    }
    private void  Spawn()
    {

        GameObject pipes = Instantiate(pipePrefab, transform.position, transform.rotation);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.transform.parent = gameObject.transform;

    }

    private void Timer(float _time)
    {
        if (_time == 0 && check == true)
        {
            check = false;
            CancelInvoke(nameof(Spawn));
#pragma warning disable CS1717 // Assignment made to same variable
            time = time;
#pragma warning restore CS1717 // Assignment made to same variable

        }
        else if( _time != 0 && check == false)
        {
            check = true;
            time -= Time.deltaTime;
            if (time == 0)
            {
                time = 1;
            }
            InvokeRepeating(nameof(Spawn), time , spawnRate);

        }
           

    }

    private void OnSpeedChanged(float speed)
    {
        moveSpeed = speed;
        //Debug.Log(moveSpeed /2 );

    }

    private void RestartGame()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
