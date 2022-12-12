using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_AI : MonoBehaviour
{
    private Transform dragon;
    private Rigidbody2D rigidbody;

    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    public float detectionDistance = 5;
    public float maxDetectionDistance = 6;
    public float minimumDistance = 0.1f;
    public float roamingRadius = 3;
    public float waitingTime = 2f;
    float waitingTimeLeft = 0;

    Transform target = null;
    Vector3 destination;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        dragon = GameObject.FindGameObjectWithTag("Player").transform;
        destination = transform.position;
    }

    void Update()
    {
        float distanceFromDragon = Vector3.Distance(dragon.position, transform.position);
        if (distanceFromDragon <= detectionDistance)
        {
            target = dragon;
        }
        else if (distanceFromDragon >= maxDetectionDistance)
        {
            target = null;
        }

        if (target == null)
        {
            if(IsAtDestination())
            {
                if(waitingTimeLeft <= 0)
                {
                    destination = (Vector3)(Random.insideUnitCircle * roamingRadius) + transform.position;
                    waitingTimeLeft = waitingTime;
                }
                else
                {
                    rigidbody.velocity = Vector2.zero;
                    waitingTimeLeft -= Time.deltaTime;
                }
            }
            else
            {
                rigidbody.velocity = (destination - transform.position).normalized * movementSpeed;
            }
        }
        else
        {
            if(IsAtDestination())
            {
                // attack!
            }
            else
            {
                destination = target.position;
                rigidbody.velocity = (destination - transform.position).normalized * movementSpeed;
            }
        }
    }

    public bool IsAtDestination()
    {
        return Vector3.Distance(transform.position, destination) <= minimumDistance;
    }
}
