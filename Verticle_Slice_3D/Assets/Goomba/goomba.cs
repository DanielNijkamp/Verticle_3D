using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class goomba : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints; //array of waypoints
    int waypointIndex; //index for choosing the poiints
    Vector3 target;
    private movement player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1) //if our distance to waypoint is less then 1 then increase the waypoint index by one
        {
            iterateWaypointIndex();
            UpdateDestination();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Attack();
        }
    }
    public void Attack()
    {
        player.health -= 10;
    }
    void UpdateDestination() //then update the destination by setting the target to current waypoint and the navmesh agent destination to target
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }
    void iterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex== waypoints.Length) //if the waypointindex is egual to the number of waypoints it's get setback to zero
        {
            waypointIndex = 0;
        }
    }
}
