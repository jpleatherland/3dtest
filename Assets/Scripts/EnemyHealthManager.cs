using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    [SerializeField] int maxHealth = 2;
    public int currentHealth;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] EnemyController enemyController;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("got hit");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(deathAnim());
        }

    }
    private IEnumerator deathAnim()
    {
        enemyAnimator.SetTrigger("isDead");
        enemyController.currentState = EnemyController.AIState.isDead;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}