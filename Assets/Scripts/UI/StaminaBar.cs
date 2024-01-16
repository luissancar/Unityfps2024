using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;

    public float maxStamina = 100;
    private float currentStamina;
    private float staminaRegenerateStaminaTime = 0.1f;
    private float regenerateAmount = 2f;
    private float losingStaminaTime = 0.1f;

    private Coroutine mCoroutineLosing;
    private Coroutine mCoroutineRegenerate;


    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount > 0)
        {
            if (mCoroutineLosing != null)
            {
                StopCoroutine(mCoroutineLosing);
            }

            mCoroutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));
            if (mCoroutineRegenerate != null)
            {
                StopCoroutine((mCoroutineRegenerate));
            }

            mCoroutineRegenerate = StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {
            FindObjectOfType<PlayerMovement>().isSprinting = false;
        }
    }

    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        while (currentStamina >= 0 && (x != 0 || z != 0)
              )
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            currentStamina -= amount;
            staminaSlider.value = currentStamina;
            yield return new WaitForSeconds(losingStaminaTime);
        }

        mCoroutineLosing = null;
        FindObjectOfType<PlayerMovement>().isSprinting = false;
        FindObjectOfType<PlayerMovement>().sprintSpeed = 1;
    }

    private IEnumerator RegenerateStaminaCoroutine()
    {
        yield return new WaitForSeconds(1);
        while (currentStamina < maxStamina)
        {
            currentStamina += regenerateAmount;
            staminaSlider.value = currentStamina;
            yield return new WaitForSeconds(staminaRegenerateStaminaTime);
        }

        mCoroutineRegenerate = null;
    }
}