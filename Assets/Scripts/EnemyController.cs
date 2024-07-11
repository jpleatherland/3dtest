using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Animator enemyAnimator;

    public enum AIState { isIdle, isPatrolling, isChasing, isAttacking, isDead };

    public AIState currentState = AIState.isIdle;

    [SerializeField] float waitAtPoint = 2f;
    private float waitCounter;

    public float targetRadius = 3f;
    public float chaseRange = 10f;
    public float attackRange = 1.5f;

    public float timeBetweenAttacks;
    private float attackCounter;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == AIState.isDead)
        {
            DeadState();
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

            if (attackCounter > 0)
            {
                attackCounter -= Time.deltaTime;
            }

            if (currentState != AIState.isChasing && currentState != AIState.isAttacking && distanceToPlayer <= targetRadius)
            {
                currentState = AIState.isChasing;
                enemyAnimator.SetBool("isMoving", true);
            }

            switch (currentState)
            {
                case AIState.isIdle:
                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIState.isPatrolling;
                        agent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
                        agent.isStopped = false;
                        enemyAnimator.SetBool("isMoving", true);
                    }
                    break;

                case AIState.isPatrolling:
                    if (agent.remainingDistance <= .2f)
                    {
                        if (currentPatrolPoint >= patrolPoints.Length - 1)
                        {
                            currentPatrolPoint = 0;
                        }
                        else
                        {
                            currentPatrolPoint++;
                        }
                        currentState = AIState.isIdle;
                        enemyAnimator.SetBool("isMoving", false);
                        waitCounter = waitAtPoint;
                    }
                    break;

                case AIState.isChasing:
                    agent.SetDestination(PlayerController.instance.transform.position);
                    if (distanceToPlayer <= attackRange)
                    {
                        currentState = AIState.isAttacking;
                        enemyAnimator.SetBool("isMoving", false);
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;
                    }
                    else if (distanceToPlayer > chaseRange)
                    {
                        waitCounter = waitAtPoint;
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;
                        currentState = AIState.isIdle;
                        enemyAnimator.SetBool("isMoving", false);
                    }
                    break;

                case AIState.isAttacking:
                    if (!isAttacking)
                    {
                        transform.LookAt(PlayerController.instance.transform, Vector3.up);
                        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                    }
                    if ((distanceToPlayer <= attackRange) && (attackCounter <= 0))
                    {
                        enemyAnimator.SetBool("isMoving", false);
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;
                        StartCoroutine(Attack());

                        attackCounter = timeBetweenAttacks;
                    }
                    else if (distanceToPlayer > attackRange)
                    {
                        agent.isStopped = false;
                        agent.SetDestination(PlayerController.instance.transform.position);
                        enemyAnimator.SetBool("isMoving", true);
                        currentState = AIState.isChasing;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        isAttacking = false;
    }

    private void DeadState()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

}
