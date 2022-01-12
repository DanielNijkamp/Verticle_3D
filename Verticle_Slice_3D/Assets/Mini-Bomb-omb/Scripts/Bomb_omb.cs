using UnityEngine;
using UnityEngine.AI;

public class Bomb_omb : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject coin;
    private PlayerTest PT;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Exploding
    public float timeToExplode = 5;
    bool isTimerOn;
    public Transform vision;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        PT = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTest>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) Explode();

        if (isTimerOn)
            TimerIsOn();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player.position);
    }

    public void Explode()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player.position);

        TimerIsOn();

        if (timeToExplode < 0)
        {
            Damageplayer();
            //DropCoin();
            DestroyEnemy();
        }
        if (timeToExplode < -1)
        {
            timeToExplode = 0;
        }
    }

    private void TimerIsOn()
    {
        isTimerOn = true;
        sightRange = 10;
        timeToExplode -= Time.deltaTime;
    }

    public void Damageplayer()
    {
        if (playerInAttackRange && playerInSightRange)
            PT.playerHP -= 1f;
    }
    public void DropCoin()
    {
        GameObject a = Instantiate(coin) as GameObject;
        a.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(vision.position, sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
