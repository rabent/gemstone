using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class special : MonoBehaviour
{
    public Coroutine init(weaponmanager wmng) {
       Coroutine crt=StartCoroutine(make_shadow(wmng));
       return crt;
    }

    IEnumerator make_shadow(weaponmanager wmng) { //그림자 생성 후 weaponmanager 파라미터 넘겨줌
        while(true) {
        GameObject shadow = gamemanager.instance.poolmng.pulling(3);
        shadow.transform.position = wmng.player.transform.position;
        weaponmanager shawmng=shadow.GetComponent<weaponmanager>();
        shawmng.damage=wmng.damage;
        shawmng.count=wmng.count;
        shawmng.prefabid=wmng.prefabid;
        shawmng.gem_color=wmng.gem_color;
        shawmng.speed=wmng.speed;
        shawmng.radius=wmng.radius;
        shawmng.penet=wmng.penet;
        shawmng.skill_use();
        StartCoroutine(shadow_false(shadow));
        yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator shadow_false(GameObject shadow) {
        yield return new WaitForSeconds(5f);
        shadow.SetActive(false);
    }
}
