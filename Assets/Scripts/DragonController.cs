using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public DragonEnergy dragonEnergy;
    public float movementSpeed = 1;
    public float rotationSpeed = 1;
    public float fatMovementSpeed = 1;

    private Animator myAnim;
    private Rigidbody2D rigidbody;
    private SpriteAnimation spriteAnim;

    public bool flying;
    public int flyingLayerIndex;

    public bool dead;

    public GameObject safeZoneBorder;
    public GameObject cloudLayer;

    [SerializeField] private GameObject flyingParticle;
    [SerializeField] private GameObject landingParticle;


    void Start()
    {
        myAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        flying = false;
        dead = false;

        spriteAnim = GetComponent<SpriteAnimation>();
    }

    void Update()
    {
        Vector2 inputValues = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.velocity = inputValues * movementSpeed;

        if(inputValues.magnitude > 0.1f)
        {
            //transform.up = Vector3.Lerp(transform.up, rigidbody.velocity.normalized, rotationSpeed * Time.deltaTime);

            spriteAnim.FlipSprite(true ? inputValues.x < 0 : false);
            if (flying)
            {
                spriteAnim.UpdateAnimation(AnimationState.FLY);
            }
            else
            {
                spriteAnim.UpdateAnimation(AnimationState.MOVE);
            }
        }
        else
        {
            if (flying)
            {
                spriteAnim.UpdateAnimation(AnimationState.FLY);
            }
            else
            {
                spriteAnim.UpdateAnimation(AnimationState.IDLE);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flying = !flying;

            if (flying)
            {
                gameObject.layer = flyingLayerIndex;
                GetComponent<DragonFlying_Visual>().Set_FlyStatus(true);

                GameObject instantiatedParticle = GameObject.Instantiate(flyingParticle);
                instantiatedParticle.transform.position = transform.position;
            }
            else
            {
                gameObject.layer = 0;
                GetComponent<DragonFlying_Visual>().Set_FlyStatus(false);

                GameObject.Instantiate(landingParticle, transform);
            }
        }

        if (flying)
        {
          //safeZoneBorder.SetActive(false);
            cloudLayer.SetActive(true);
        }

        if(!flying)
        {
         // safeZoneBorder.SetActive(true);
            cloudLayer.SetActive(false);
        }
    }
}
