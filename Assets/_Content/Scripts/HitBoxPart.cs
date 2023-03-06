using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitBoxPart : MonoBehaviour, IDamagable
{
   [HideInInspector] public Enemy enemy;
   private Rigidbody _rb;
   

   private void Awake()
   {
      _rb = GetComponent<Rigidbody>();
   }

   public void TakeHit(float force, Vector3 hitPoint, Vector3 hitDirection)
   {
      enemy.Die();
      _rb.AddForce(hitDirection * force);
   }
}
