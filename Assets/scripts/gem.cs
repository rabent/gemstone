using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    [SerializeField]
    private gemData gemData;
    public gemData GemData {
        get {return gemData;}
        set {gemData=value;}}
}
