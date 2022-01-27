using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Transform startpoint, endpoint;

    [SerializeField]
    float changeDirectionDelay;

    private Transform destinationTarget, departTarget;

    private float startTime;

    private float journeyLength;

    bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        departTarget = startpoint;
        destinationTarget = endpoint;

        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
            }
            else
            {
                isWaiting = true;
                StartCoroutine(changeDelay());
            }
        }
    }
    void changeDestination()
    {
        if(departTarget == endpoint && destinationTarget == startpoint)
        {
            departTarget = startpoint;
            destinationTarget = endpoint;
        }
        else
        {
            departTarget = endpoint;
            destinationTarget = startpoint;
        }
    }
    IEnumerator changeDelay()
    {
        yield return new WaitForSeconds(changeDirectionDelay);
        changeDestination();
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }
}
