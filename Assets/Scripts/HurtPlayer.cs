using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int damage;
    public float knockbackLength;
    public Vector2 knockbackPower;
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
        if (other.CompareTag("Player"))
        {
            HealthManager.instance.Hurt(damage, knockbackLength, knockbackPower);
        }
    }
}
