using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healAmount;
    public bool isFullHeal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if(isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            } else
            {
                HealthManager.instance.AddHealth(healAmount);
            }
        }
    }
}
