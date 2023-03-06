using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public List<Enemy> enemies;

    [SerializeField] private bool isFirstPlatform;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy e))
        {
            enemies.Add(e);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.01f);
        foreach (Enemy item in enemies)
        {
            item.onDie.AddListener(OnEnemyDie);
        }
    }

    private void OnDestroy()
    {
        foreach (Enemy item in enemies)
        {
            item.onDie.RemoveListener(OnEnemyDie);
        }
    }

    void OnEnemyDie(Enemy e)
    {
        int diedCount = 0;
        foreach (Enemy item in enemies)
        {
            diedCount += item.isDied ? 1 : 0;
        }

        if (diedCount >= enemies.Count)
        {
            GameManager.instance.player.DoMove();
        }
    }
}
