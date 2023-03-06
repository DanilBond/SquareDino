using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;

    [SerializeField] private Rigidbody rb;
    
    private Collider _collider;

    public void Initialize(Vector3 targetDirection)
    {
        transform.parent = null;
        _collider = gameObject.AddComponent<BoxCollider>();
        rb.useGravity = false;
        rb.angularDrag = 0;
        rb.AddForce((targetDirection - transform.position).normalized * projectileData.throwForce);
        rb.transform.LookAt(targetDirection, Vector3.up);
        rb.AddTorque(transform.right * projectileData.throwForce);
        
        //Invoke(nameof(Return), 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collider.enabled = false;
        rb.isKinematic = true;
        transform.parent = collision.gameObject.transform;
        
        if (collision.gameObject.TryGetComponent(out HitBoxPart hbp))
        {
            hbp.TakeHit(projectileData.discardingForce, collision.contacts[0].point, -collision.contacts[0].normal);
            GameObject blood = Instantiate(projectileData.particles[Random.Range(0, projectileData.particles.Length)],
                transform.position, Quaternion.identity);
        }
        
    }

    private void Return()
    {
       //FindObjectOfType<ObjectPooling>().Push("Projectille", gameObject);
    }
}
