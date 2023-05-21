using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;

    float timer;

    void Awake() 
    {
        spwanPoint = GetComponentsInChildren<Transform>();    
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
       GameObject enemy = gamemanager.instance.pool.pulling(Random.Range(2, 4));
       enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
    }
}
