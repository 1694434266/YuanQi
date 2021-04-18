using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Transform player;
    private Vector3 direction;
    void Start()
    {
        
    }
    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        //计算player到camera的方向向量的距离
        direction = player.position - transform.position;
    }

    void Update()
    {
        transform.position = player.position - direction;
    }
}
