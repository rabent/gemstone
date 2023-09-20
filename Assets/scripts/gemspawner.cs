using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemspawner : MonoBehaviour
{
    public GameObject gemprefab;
    public gemData[] gems; //현존하는 모든 젬을 담을 배열

    public GameObject gem_spawn() {
        GameObject gem=Instantiate(gemprefab);
        gem g=gem.GetComponent<gem>();
        int i=Random.Range(0,gems.Length);
        g.GemData=gems[i]; //젬 생성 후 랜덤으로 젬 데이터를 넣어줌
        gem.GetComponent<SpriteRenderer>().sprite=g.GemData.spr;
        return gem;
    }
}
