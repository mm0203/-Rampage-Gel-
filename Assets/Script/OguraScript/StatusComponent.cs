using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class StatusComponent : MonoBehaviour
{
    public int Level { get; set; }
    public float HP { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
}
