using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class gemData : ScriptableObject
{
   
    // Start is called before the first frame update
    public bool isactive;
    public bool ispassive;
    public bool isspecial;
    public int color;
    public int id;
    public float damage;
    public int count;
    public int speed;
    public Sprite spr;
    public float radius;
}
