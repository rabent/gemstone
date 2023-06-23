using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemspawner : MonoBehaviour
{
    public GameObject gemprefab;
    public gemData[] gems;

    public GameObject gem_spawn() {
        GameObject gem=Instantiate(gemprefab);
        gem g=gem.GetComponent<gem>();
        int i=Random.Range(0,gems.Length);
        g.GemData=gems[i];
        gem.GetComponent<SpriteRenderer>().sprite=g.GemData.spr;
        return gem;
    }
}
