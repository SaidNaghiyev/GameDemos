using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color[] colors;

    private void Start()
    {
        GetComponent<Renderer>().material.color = colors[1];
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0f, 0.5f, 0f);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GetComponent<Renderer>().material.color = colors[0];

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GetComponent<Renderer>().material.color = colors[1];
        }
    }
}
