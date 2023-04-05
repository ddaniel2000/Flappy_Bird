using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment_Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enviroment;
    private void OnEnable()
    {
        Enviroment_Movement.OnTrigger += SpawnEnviroment;
    }
    private void OnDisable()
    {
        Enviroment_Movement.OnTrigger -= SpawnEnviroment;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnviroment()
    {
       
        Instantiate(enviroment, transform.position, transform.rotation);
    }
}
