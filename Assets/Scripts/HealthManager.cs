using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth, maxHealth;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hurt(int damage, float knockbackLength, Vector2 knockbackPower)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.instance.Respawn();
        } else 
        {
            PlayerController.instance.triggerKnockback(knockbackLength, knockbackPower);
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
