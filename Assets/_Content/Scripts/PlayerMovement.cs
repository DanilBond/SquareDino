using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider levelSlider;
    
    private List<Transform> _waypoints = new List<Transform>(0);

    private bool isMoving;
    private int currentWaypoint;
    private float startDist;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MoveToNext();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        FindWaypoints();
        StartCoroutine(MoveUpdate());
    }

    void FindWaypoints()
    {
        GameObject[] way = GameObject.FindGameObjectsWithTag("Waypoint");

        Dictionary<GameObject, float> temp = new Dictionary<GameObject, float>(0);
        
        foreach (GameObject w in way)
        {
            temp.Add(w, Vector3.Distance(transform.position, w.transform.position));
        }
        
        foreach (KeyValuePair<GameObject, float> author in temp.OrderBy(key => key.Value))
        {
            _waypoints.Add(author.Key.transform);
        }
    }

    public void MoveToNext()
    {
        isMoving = true;   
        player.armsAnim.SetBool("Move",true);
        float speed = (Vector3.Distance(transform.position, _waypoints[currentWaypoint].position) / 10) *
                      player.playerData.moveDuration;
        transform.DOMove(_waypoints[currentWaypoint].position, speed).OnComplete(() =>
        {
            player.armsAnim.SetBool("Move",false);
            transform.DOLookAt(_waypoints[currentWaypoint + 1].position, player.playerData.lookAtDuration).OnComplete(() =>
            {
                player.armsAnim.SetTrigger("Jump");
                transform.DOJump(_waypoints[currentWaypoint + 1].position, player.playerData.jumpPower, 1, player.playerData.jumpDuration).OnComplete(() =>
                {
                    transform.DOLookAt(_waypoints[currentWaypoint + 1].position, player.playerData.lookAtDuration).OnComplete(() =>
                    {
                        isMoving = false;});
                    currentWaypoint+=2;
                    if (currentWaypoint > _waypoints.Count - 2)
                    {
                        currentWaypoint = _waypoints.Count;
                        GameManager.instance.uiManager.OnFinish();
                    }
                }).SetEase(player.playerData.jumpEase);
            });
            
        }).SetEase(player.playerData.moveEase);
    }

    IEnumerator MoveUpdate()
    {        
        startDist = Vector3.Distance(transform.position, GameManager.instance.worldGeneration.finishPlatformObject.position);

        while (true)
        {
            yield return new WaitForSeconds(.05f);
            if (isMoving)
            {
                float dist = Vector3.Distance(transform.position, GameManager.instance.worldGeneration.finishPlatformObject.position);
                levelSlider.value = 1 - dist / startDist;
            }
        }
    }
}
