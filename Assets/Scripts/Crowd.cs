using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public string targetTag;
    public GameObject Target;
    public GameObject[] AllTargets;

    private void Awake()
    {
        navMeshAgent= GetComponent<NavMeshAgent>();
        float offset = Random.Range(-3f, 0.5f);
        navMeshAgent.speed= MainMechanic.Instance.getSpeed()+offset;
    }

    private void Start()
    {
        FindTarget();
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector3.Distance(this.transform.position, Target.transform.position) <= 0.5f)
            {
                FindTarget();
            }
        }
    }


    public void FindTarget (){

        if (Target != null) {
            //returning target tag back to normal once its reached
            Target.transform.tag = targetTag;
        }

        AllTargets = GameObject.FindGameObjectsWithTag(targetTag);
        Target = AllTargets[Random.Range(0, AllTargets.Length)];
        Target.transform.tag = targetTag;
        navMeshAgent.destination= Target.transform.position;
    }

    public void resetTarget() {
        Target.transform.tag = targetTag;
    }
}
