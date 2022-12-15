using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EngagementState
{
    IGNORE , ATTACK , RUN_AWAY
}

public class Villager_AI : MonoBehaviour
{
    private Transform dragon;
    private Rigidbody2D rigidbody;
    private SpriteAnimation spriteAnim;

    public EngagementState engagementState;

    [Space(10)]

    [Header("Movement Properties")]
    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    [Space(10)]

    [Header("Roaming Properties")]
    public bool canRoam = true;
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

        spriteAnim = GetComponent<SpriteAnimation>();
    }

    void Update()
    {
        if(dragon)
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
        }

        if (target == null)
        {
            if(canRoam)
            {
                if (IsAtDestination())
                {
                    if (waitingTimeLeft <= 0)
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
        }
        else
        {
            switch (engagementState)
            {
                case EngagementState.ATTACK:
                    {
                        if (IsAtDestination())
                        {
                            // attack!
                            if (nextAttack <= 0)
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
                        break;
                    }
                case EngagementState.RUN_AWAY:
                    {
                        destination = (transform.position - target.position).normalized * 10f;
                        rigidbody.velocity = (destination - transform.position).normalized * movementSpeed;
                        break;
                    }
            }
        }

        if(nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }

        if(spriteAnim)
        {
            // is moving?
            if (rigidbody.velocity.magnitude > 0.1f)
            {
                spriteAnim.FlipSprite(true ? rigidbody.velocity.x < 0 : false);
                spriteAnim.UpdateAnimation(AnimationState.MOVE);
            }
            else
            {
                spriteAnim.UpdateAnimation(AnimationState.IDLE);
            }
        }
    }

    public void Attack()
    {
        if(target == dragon && Vector3.Distance(transform.position , target.position) <= attackDistance
            && !target.GetComponent<DragonController>().flying)
        {
            dragon.GetComponent<DragonHealth>().AddDamage(attackDamage);
        }
    }

    public bool IsAtDestination()
    {
        return Vector3.Distance(transform.position, destination) <= minimumDistance;
    }
}
