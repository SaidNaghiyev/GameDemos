using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroyer : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.parent.parent.gameObject);      //Destroys everything which collides with gameObject
    }
}
