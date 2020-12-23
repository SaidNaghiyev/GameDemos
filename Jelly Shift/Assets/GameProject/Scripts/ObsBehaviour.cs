using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsBehaviour : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // search for player
    }

    void Update()
    {
        transform.localScale = player.localScale; //and get it's scale
    }
}
