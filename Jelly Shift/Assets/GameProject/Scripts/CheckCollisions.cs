using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))//if collide with obstacle change scene
        {
            GameManager.instance.Die();
        }

        if (other.CompareTag("Score"))//Score add more point
        {
            GameManager.instance.addScore(1);
        }
    }
}
