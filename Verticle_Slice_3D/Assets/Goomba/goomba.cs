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
    public GameObject coin;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Goomba Stats
    public bool isInAttackRange, isInSightRange, inChargeRange;
    public float sightRange, attackRange, chargeRange;
    public Transform sight;

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
        isInSightRange = Physics.CheckSphere(sight.position, sightRange, whatIsPlayer);
        isInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        inChargeRange = Physics.CheckSphere(sight.position, chargeRange, whatIsPlayer);

        if (!isInAttackRange && !isInSightRange) UpdateDestination();
        if (!isInAttackRange && isInSightRange) ChasePlayer();
        if (isInAttackRange && isInSightRange) AttackPlayer();
    }

    //Primary Functions
    public void UpdateDestination() 
    {
        Debug.Log("This Works function");
        if (agent.speed != 0.2f) agent.speed = 0.5f;

        if (!walkPointSet) SearchWalkPoint(); Debug.Log("This Works function finding ground");

        if (walkPointSet) agent.SetDestination(walkPoint); Debug.Log("This Works function setting position");

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false; Debug.Log("This Works function walkpoint reached");
    }

    public void ChasePlayer()
    {
        agent.speed = 0.2f;
        agent.SetDestination(player.position);
        transform.LookAt(player.position);
        if (inChargeRange)
        {
            agent.speed = 10;
        }
    }

    public void AttackPlayer()
    {
        //speed 0 
        //play animation and change speed to 10

        StartCoroutine(Attack());
    }

    //Secondary Functions
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(Death());
        }
    }
    IEnumerator Attack()
    {
        for (int i = 0; i < 1; i++)
        {
            _player.health -= 10;
            yield return new WaitForSeconds(5f);
        }
    }

     public IEnumerator Death()
    {
        //GetComponent<Animator>().SetBool("Squish", true);
        //GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<CapsuleCollider>().enabled = false;
        //GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(.5f);
        //Dropcoin();
        Destroy(gameObject);
    }

    public void Dropcoin()
    {
        GameObject a = Instantiate(coin) as GameObject;
        a.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sight.position, sightRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(sight.position, chargeRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
