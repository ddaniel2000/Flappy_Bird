using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update the camrea position on x axis 
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
    }
}
