using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class goomba : MonoBehaviour
{
    public NavMeshAgent agent;
 
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private movement _player;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Goomba Stats
    public bool isInAttackRange, isInSightRange;
    public float sightRange, attackRange;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        isInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        isInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!isInAttackRange && !isInSightRange) UpdateDestination();
        if (!isInAttackRange && isInSightRange) ChasePlayer();
        if (isInAttackRange && isInSightRange) AttackPlayer();
    }

    //Primary Functions
    void UpdateDestination() 
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player.position);
    }

    public void AttackPlayer()
    {

    }

    //Secondary Functions
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Attack();
        }
    }
    public void Attack()
    {
        _player.health -= 10;
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
