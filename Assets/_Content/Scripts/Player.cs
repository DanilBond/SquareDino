using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public PlayerData playerData;
    public Animator armsAnim;
    
    [HideInInspector] public Platform currentPlatform;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerThrowing playerThrowing;

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out Platform p))
        {
            currentPlatform = p;
        }
    }

    public void DoMove()
    {
        playerMovement.MoveToNext();
    }
}