using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_AI : MonoBehaviour
{
    private Transform dragon;
    private Rigidbody2D rigidbody;

    [Header("Movement Properties")]
    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    [Space(10)]

    [Header("Roaming Properties")]
    public float detectionDistance = 5;
    public float maxDetectionDistance = 6;
    public float minimumDistance = 0.1f;
    public float roamingRadius = 3;
    public float waitingTime = 2f;
    float waitingTimeLeft = 0;

    [Space(10)]

    [Header("Combat Properties")]
    public float attackDistance = 1;
    public float attackDamage = 25;
    public float attackRate = 1;    // seconds
    float nextAttack = 0;

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
            destination = target.position;
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
                if(nextAttack <= 0)
                {
                    Attack();
                    nextAttack = attackRate;
                }
            }
            else
            {
                destination = target.position;
                rigidbody.velocity = (destination - transform.position).normalized * movementSpeed;
            }
        }

        if(nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(target == dragon && Vector3.Distance(transform.position , target.position) <= attackDistance)
        {
            dragon.GetComponent<DragonHealth>().AddDamage(attackDamage);
        }
    }

    public bool IsAtDestination()
    {
        return Vector3.Distance(transform.position, destination) <= minimumDistance;
    }
}
