using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private GameObject startPlatform;
    [SerializeField] private GameObject finishPlatform;
    [SerializeField] private GameObject[] platforms;

    [SerializeField] private int minLength, maxLength;
    [SerializeField] private Vector3 minDist, maxDist;
    
    [Space(10), Header("Enemies")]
    [SerializeField] private Enemy enemy;
    [SerializeField, Range(0,100)] private float spawnChance;

    [HideInInspector] public Transform finishPlatformObject;
    
    private void Awake()
    {
        Generate();
    }

    void Generate()
    {
        int len = Random.Range(minLength, maxLength);

        Vector3 prevPos = startPlatform.transform.position;
        
        for (int i = 0; i < len; i++)
        {
            Vector3 pos = prevPos + new Vector3(
                Random.Range(minDist.x, maxDist.x),
                0,
                Random.Range(minDist.z, maxDist.z));
            pos.y = Random.Range(minDist.y, maxDist.y);

            GameObject platform = platforms[Random.Range(0, platforms.Length)];
            GameObject p = Instantiate(platform, pos, platform.transform.rotation);

            prevPos = p.transform.position;
        }
        
        Vector3 pos1 = prevPos + new Vector3(
            Random.Range(minDist.x, maxDist.x),
            0,
            Random.Range(minDist.z, maxDist.z));
        pos1.y = Random.Range(minDist.y, maxDist.y);

        
        GameObject p1 = Instantiate(finishPlatform, pos1, finishPlatform.transform.rotation);
        finishPlatformObject = p1.transform;
        
        SpawnEnemies();
    }
    void SpawnEnemies()
    {
        GameObject[] poses = GameObject.FindGameObjectsWithTag("StickmanPos");

        for (int i = 0; i < poses.Length; i++)
        {
            if (Random.Range(0, 100) < spawnChance)
            {
                GameObject enemy = null;
                enemy = Instantiate(this.enemy.gameObject, poses[i].transform.position, this.enemy.gameObject
                .transform.rotation);
            }
        } 
    }
}
