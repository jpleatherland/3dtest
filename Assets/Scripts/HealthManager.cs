using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth, maxHealth;

    public float invicibleLength = 2f;
    private float invicibleCounter;

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
        if (invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;
            if(Mathf.Floor(invicibleCounter * 5f) % 2 == 0)
            {
                PlayerController.instance.playerPiece.SetActive(true);
            } else 
            {
                PlayerController.instance.playerPiece.SetActive(false);
            }
            if (invicibleCounter <= 0)
            {
                PlayerController.instance.playerPiece.SetActive(true);
            }
        }
    }

    public void Hurt(int damage, float knockbackLength, Vector2 knockbackPower)
    {
        if (invicibleCounter <= 0)
        {
            currentHealth -= damage;
            UIManager.instance.RemoveHearts(damage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.triggerKnockback(knockbackLength, knockbackPower);
                invicibleCounter = invicibleLength;

                PlayerController.instance.playerPiece.SetActive(false);
            }
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.AddHearts(maxHealth);
    }

    public void AddHealth(int healAmount)
    {
        currentHealth += healAmount;
        UIManager.instance.AddHearts(healAmount);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
