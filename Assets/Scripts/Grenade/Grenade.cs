using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 2f;
    private float countdown;
    public float radio = 5f;
    public float fuerzaExplosion = 70f;
    private bool exploted = false;

    //Sonidos
    public AudioClip shotSound;
    public AudioSource shotAudioSource;

    //Efectos
    public GameObject efectoExplosion;
    private GameObject explosionEf;


    private void Awake()
    {
        shotAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        countdown = delay;
    }


    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !exploted)
        {
            Exploted();
            exploted = true;
        }

        if (exploted && !shotAudioSource.isPlaying)
        {
            Destroy(explosionEf);
            Destroy(gameObject);
        }
           
    }

    private void Exploted()
    {
        shotAudioSource.PlayOneShot(shotSound);
        
        explosionEf = Instantiate(efectoExplosion, transform.position, transform.rotation);
       
        
        Collider[] colliders = Physics.OverlapSphere(transform.position,
            radio);
        foreach (var objeto in colliders)
        {
            Rigidbody rb = objeto.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaExplosion, transform.position, radio);
            }
        }

        
        Destroy(transform.GetChild(0).gameObject);
    }
}