using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float moveDuration;
    public float jumpDuration;
    public float jumpPower;
    public float lookAtDuration;

    public Ease jumpEase;
    public Ease moveEase;
}
