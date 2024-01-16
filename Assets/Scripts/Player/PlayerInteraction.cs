using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI textAmmo;
    public TextMeshProUGUI textVidas;
    public TextMeshProUGUI textGrenades;

    public bool vulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
        textVidas.text = GameManager.instance.vidas.ToString();
        textAmmo.text = GameManager.instance.gunAmmo.ToString();
        textGrenades.text = GameManager.instance.grenades.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo") && vulnerable)
        {
            PerderVida();
        }

        if (other.CompareTag("AmmoBox"))
        {
            GameManager.instance.gunAmmo += other.GetComponent<AmmoBox>().ammo;
            textAmmo.text = GameManager.instance.gunAmmo.ToString();
            Destroy(other.gameObject);
        }
    }

    void PerderVida()
    {
        vulnerable = false;
        GameManager.instance.vidas -= 2;
        if (GameManager.instance.vidas <= 0)
            Muerto();
        else
        {
            textVidas.text = GameManager.instance.vidas.ToString();
            StartCoroutine(VulnerableCorrutina());
        }
    }

    private void Muerto()
    {
        GameManager.instance.muerto = true;
        textVidas.text = "0";
        Transform transformacion = transform;
        float anguloX = -128f;
        transformacion.eulerAngles =
            new Vector3(anguloX, transformacion.eulerAngles.y,
                transformacion.eulerAngles.z);
        StartCoroutine(Reiniciar());
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator VulnerableCorrutina()
    {
        yield return new WaitForSeconds(3);
        vulnerable = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BulletEnemy"))
        {
            PerderVida();
        }
    }
}