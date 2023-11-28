using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
    public GameObject[] SpawnObjects;
    public Transform parentObject;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newObject = SpawnObjects[Random.Range(0,SpawnObjects.Length)];
        Instantiate(newObject,transform.position,Quaternion.identity,parentObject);
        Destroy(gameObject);
        
    }

}
