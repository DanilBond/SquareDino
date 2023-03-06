using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] ragdoll;

    [SerializeField] private SkinnedMeshRenderer skin;
    [SerializeField] private Mesh[] skinMeshes;

    public UnityEvent<Enemy> onDie;

    [HideInInspector] public bool isDied;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        RandomizeEnemy();
        ragdoll.ToList().ForEach(rb => {rb.gameObject.AddComponent<HitBoxPart>().enemy = this;});
        animator.SetBool(Random.Range(1,4).ToString(), true);
        SetRagdoll(false);
    }

    public void Die()
    {
        isDied = true;
        SetRagdoll(true);
        onDie?.Invoke(this);
    }

    public void SetRagdoll(bool state)
    {
        animator.enabled = !state;
        ragdoll.ToList().ForEach(rb => { rb.isKinematic = !state;});
    }

    void RandomizeEnemy()
    {
        skin.sharedMesh = skinMeshes[Random.Range(0, skinMeshes.Length)];
    }
}
