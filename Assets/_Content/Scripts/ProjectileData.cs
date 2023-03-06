using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileData : ScriptableObject
{
    public float throwForce;
    public float discardingForce;

    public GameObject[] particles;
}
