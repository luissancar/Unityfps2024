using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject efectoExplosion;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Instantiate(efectoExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(this);
        }
    }
}
