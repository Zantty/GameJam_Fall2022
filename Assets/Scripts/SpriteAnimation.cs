using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationState
{
    IDLE , MOVE , FLY
}

public class SpriteAnimation : MonoBehaviour
{
    [Header("Animations Sprites")]
    public List<Sprite> idleSprites;
    private int idleIndex = 0;

    public List<Sprite> movementSprites;
    private int movementIndex = 0;

    public List<Sprite> flyingSprites;
    private int flyingIndex = 0;


    [Space(10)]

    [Header("Animations Properties")]
    public float frameRate = 0.5f;     // seconds
    float nextFrame = 0;
    public AnimationState animationState = AnimationState.IDLE;

    private SpriteRenderer spriteRenderer;

    public void UpdateAnimation(AnimationState state)
    {
        animationState = state;
        switch (animationState)
        {
            case AnimationState.IDLE:
                {
                    spriteRenderer.sprite = idleSprites[idleIndex];
                    break;
                }
            case AnimationState.MOVE:
                {
                    spriteRenderer.sprite = movementSprites[movementIndex];
                    break;
                }
            case AnimationState.FLY:
                {
                    spriteRenderer.sprite = flyingSprites[flyingIndex];
                    break;
                }
        }
    }

    public void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(nextFrame > 0)
        {
            nextFrame -= Time.deltaTime;
        }
        else
        {
            if(idleSprites.Count - 1 <= idleIndex)
            {
                idleIndex = 0;
            }
            else
            {
                idleIndex++;
            }

            if (movementSprites.Count - 1 <= movementIndex)
            {
                movementIndex = 0;
            }
            else
            {
                movementIndex++;
            }

            if (flyingSprites.Count - 1 <= flyingIndex)
            {
                flyingIndex = 0;
            }
            else
            {
                flyingIndex++;
            }

            nextFrame = frameRate;
        }
    }

}
