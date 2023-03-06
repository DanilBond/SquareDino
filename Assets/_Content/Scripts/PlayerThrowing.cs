using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private Projectile throwingItemPrefab;
    [SerializeField] private Transform throwingItemPosition;

    private Projectile _projectille;
    private Vector3 _targetDirection;

    private static readonly string THROW = "Throw";

    private void Start()
    {
        Init();
    }

    void Init()
    {
        if (throwingItemPosition.transform.childCount > 0)
            _projectille = throwingItemPosition.transform.GetChild(0).GetComponent<Projectile>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                _targetDirection = hit.point;
                Throw();
            }
        }
    }

    void Throw()
    {
        if (throwingItemPosition.transform.childCount > 0)
            player.armsAnim.Play(THROW);
    }

    void OnReload()
    {
        _projectille = Instantiate(throwingItemPrefab, throwingItemPosition);
        //_projectille = FindObjectOfType<ObjectPooling>().Pull("Projectille").GetComponent<Projectile>();
    }
    
    public void OnThrowed()
    {
        _projectille.Initialize(_targetDirection);
    }
}
