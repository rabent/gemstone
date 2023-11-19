using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;
    public SpawnData[] spawnData;
    public int stage=1;
    public int level;
    public bool boss_spawned=false;
    public Dictionary<Tuple<int,int>,int[]> dic;
    float timer;

    void Awake() 
    {
        spwanPoint = GetComponentsInChildren<Transform>();  
        dic=new Dictionary<Tuple<int,int>,int[]>();
        dic_add();  
    }

    void dic_add() {
        dic.Add(new Tuple<int, int>(1,0),new int[]{0});
        dic.Add(new Tuple<int, int>(1,1),new int[]{0,1});
        dic.Add(new Tuple<int, int>(1,2),new int[]{0,1,3});
        dic.Add(new Tuple<int, int>(2,0),new int[]{0});
        dic.Add(new Tuple<int, int>(2,1),new int[]{7,1});
        dic.Add(new Tuple<int, int>(2,2),new int[]{0,1,6});
        dic.Add(new Tuple<int, int>(3,0),new int[]{7,8});
        dic.Add(new Tuple<int, int>(3,1),new int[]{7,8,9});
        dic.Add(new Tuple<int, int>(3,2),new int[]{7,8,9,5});
        dic.Add(new Tuple<int, int>(4,0),new int[]{10,11});
        dic.Add(new Tuple<int, int>(4,1),new int[]{10,11,12});
        dic.Add(new Tuple<int, int>(4,2),new int[]{10,11,12,4});
    }

    void Update() //게임 진행 시간에 따라 게임의 스테이지 레벨이 증가
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(gamemanager.instance.gameTime / 120f), 2);

        if(level<spawnData.Length && timer > spawnData[level+stage/3].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn() //풀에서 몬스터를 정해진 스폰포인트에서 랜덤하게 pulling
    {
        var t=new Tuple<int, int>(stage,level);
        int[] arr=dic[t]; int leng=arr.Length;
        int rand=Random.Range(0,leng);
        int idx=arr[rand];
        if(spawnData[idx].is_boss) {
            if(boss_spawned) {
                GameObject Enemy = gamemanager.instance.poolmng.pulling(2);
                Enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
                Enemy.GetComponent<Enemy>().Init(spawnData[0]);
            }
            else {
                GameObject Enemy = gamemanager.instance.poolmng.pulling(2);
                Enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
                Enemy.GetComponent<Enemy>().Init(spawnData[idx]);
                boss_spawned=true;
            }
        }
        else {
            GameObject Enemy = gamemanager.instance.poolmng.pulling(2);
            Enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
            Enemy.GetComponent<Enemy>().Init(spawnData[idx]);
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public float damage;
    public float fireres;
    public float iceres;
    public float lightres;
    public int gold;
    public bool is_boss=false;
}
