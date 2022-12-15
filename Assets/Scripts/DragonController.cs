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
    bool isLanding = false;

    public bool dead;

    public GameObject safeZoneBorder;
    public GameObject cloudLayer;
    public CanvasGroup cloudCanvas;
    float cloudOpacity = 0;

    [SerializeField] private GameObject flyingParticle;
    [SerializeField] private GameObject landingParticle;

    bool walking = true;

    public AudioSource walkingAudio;
    public AudioSource flyingAudio;
    public AudioSource landingAudio;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        flying = false;
        dead = false;

        spriteAnim = GetComponent<SpriteAnimation>();
        cloudCanvas.alpha = 0;
    }

    void Update()
    {
        Vector2 inputValues = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.velocity = inputValues * movementSpeed;

        if(inputValues.magnitude > 0.1f)
        {
            walking = true;
            if (walking && !flying)
            {
                if (!walkingAudio.isPlaying)
                {
                    walkingAudio.Play();
                }
            }
            //transform.up = Vector3.Lerp(transform.up, rigidbody.velocity.normalized, rotationSpeed * Time.deltaTime);

            spriteAnim.FlipSprite(true ? inputValues.x < 0 : false);
            if (isLanding)
            {
                spriteAnim.UpdateAnimation(AnimationState.LANDING);
            }
            else if(flying)
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
            if (isLanding)
            {
                spriteAnim.UpdateAnimation(AnimationState.LANDING);
            }
            else if (flying)
            {
                spriteAnim.UpdateAnimation(AnimationState.FLY);
            }
            else
            {
                spriteAnim.UpdateAnimation(AnimationState.IDLE);
                walking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isLanding)
        {
            if (!dragonEnergy.incapacitated)
            {
                if (!flying)
                {
                    flying = true;
                    gameObject.layer = flyingLayerIndex;
                    flyingAudio.Play();
                    GetComponent<DragonFlying_Visual>().Set_FlyStatus(true);

                    cloudLayer.SetActive(true);
                    cloudOpacity = 1;

                    GameObject instantiatedParticle = GameObject.Instantiate(flyingParticle);
                    instantiatedParticle.transform.position = transform.position;
                }
                else
                {
                    gameObject.layer = 0;
                    GetComponent<DragonFlying_Visual>().Set_FlyStatus(false);
                    StartCoroutine(landingSequence());
                    flyingAudio.Stop();

                    cloudLayer.SetActive(false);
                    cloudOpacity = 0;
                }
            }
            else
            {
                Debug.Log("Too full, cannot fly.");
            }
        }

        if(dragonEnergy.incapacitated && flying)
        {
            gameObject.layer = 0;
            GetComponent<DragonFlying_Visual>().Set_FlyStatus(false);
            StartCoroutine(landingSequence());
            flyingAudio.Stop();

            cloudLayer.SetActive(false);
            cloudOpacity = 0;
        }

        cloudCanvas.alpha = Mathf.Lerp(cloudCanvas.alpha, cloudOpacity, Time.deltaTime);
    }

    IEnumerator landingSequence()
    {
        isLanding = true;
        yield return new WaitForSeconds(0.75f);

        flyingAudio.Stop();
        flying = false;
        isLanding = false;
        GameObject.Instantiate(landingParticle, transform);
        landingAudio.Play();
    }
}
