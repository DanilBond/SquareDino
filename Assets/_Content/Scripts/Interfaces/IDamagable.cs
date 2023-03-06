using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeHit(float force, Vector3 hitPoint, Vector3 hitDirection);
}
