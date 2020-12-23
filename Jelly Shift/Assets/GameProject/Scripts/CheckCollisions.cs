using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    public GameObject partClick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))//if collide with obstacle change scene
        {
            GameManager.instance.Die();
        }

        if (other.CompareTag("Score"))//Score add more point
        {
            Destroy(Instantiate(partClick, other.transform.position, partClick.transform.rotation, other.transform), 2f);
            GameManager.instance.addScore(1);
        }
    }
}
