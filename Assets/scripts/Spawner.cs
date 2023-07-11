using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;
    public SpawnData[] spawnData;
    public int level;
    float timer;

    void Awake() 
    {
        spwanPoint = GetComponentsInChildren<Transform>();    
    }

    void Update() //�ð��� ���� ���� ����
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(gamemanager.instance.gameTime / 10f), spawnData.Length);

        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn() //�� ������Ʈ Ǯ��
    {
        GameObject Enemy = gamemanager.instance.pool.pulling(2);
        Enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
        Enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public float fireres;
    public float iceres;
    public float lightres;
}
