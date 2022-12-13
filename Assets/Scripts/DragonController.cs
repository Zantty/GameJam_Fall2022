using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public DragonEnergy dragonEnergy;
    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    private Animator myAnim;
    private Rigidbody2D rigidbody;

    public bool flying;
    public bool dead;
    void Start()
    {
        myAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        flying = false;
        dead = false;
    }

    void Update()
    {
        Vector2 inputValues = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.velocity = inputValues * movementSpeed;

        if(inputValues.magnitude > 0.1f)
        {
            transform.up = Vector3.Lerp(transform.up, rigidbody.velocity.normalized, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flying = !flying;
        }
    }
}
