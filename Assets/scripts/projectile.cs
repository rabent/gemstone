using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float damage;
    public int penet;

    public void init(float dam, int pen) {
        this.damage=dam;
        this.penet=pen;
    }
}
