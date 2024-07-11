using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got collide");
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("With enemy collide");
            other.GetComponent<EnemyHealthManager>().TakeDamage(damage);
        }
    }
}
