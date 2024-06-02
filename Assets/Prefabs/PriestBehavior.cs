using UnityEngine;
using UnityEngine.AI;

public class PriestBehavior : MonoBehaviour
{
    public float attackRange = 10.0f;
    public float attackCooldown = 10.0f;
    public GameObject exorcismEffect; 

    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, MainMechanic.Instance.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            AttackPlayer();
        }
        else
        {
            // Patrol or idle behavior
            agent.isStopped = false;
        }
    }

    void AttackPlayer()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            if (exorcismEffect != null)
            {
                float randomYOffset = Random.Range(5.0f, 10.0f);
                Vector3 effectPosition = MainMechanic.Instance.transform.position + new Vector3(0, randomYOffset, 0);
                Instantiate(exorcismEffect, effectPosition, Quaternion.identity);
            }

            lastAttackTime = Time.time;
        }
    }
}