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

    void Update() //게임 진행 시간에 따라 게임의 스테이지 레벨이 증가
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(gamemanager.instance.gameTime / 20f), spawnData.Length);

        if(level<spawnData.Length && timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn() //풀에서 몬스터를 정해진 스폰포인트에서 랜덤하게 pulling
    {
        GameObject Enemy = gamemanager.instance.poolmng.pulling(2);
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
    public int gold;
}
