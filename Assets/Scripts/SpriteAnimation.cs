using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationState
{
    IDLE , MOVE , FLY , LANDING
}

public enum DragonStatus
{
    NORMAL , FAT , SKINNY
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

    public List<Sprite> landingSprites;
    private int landingIndex = 0;

    [Space(10)]

    [Header("Dragon Sprites")]
    public List<Sprite> normalIdleSprites;
    public List<Sprite> fatIdleSprites;
    public List<Sprite> skinnyIdleSprites;

    public List<Sprite> normalMovementSprites;
    public List<Sprite> fatMovementSprites;
    public List<Sprite> skinnyMovementSprites;

    DragonStatus dragonStatus = DragonStatus.SKINNY;

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
            case AnimationState.LANDING:
                {
                    spriteRenderer.sprite = landingSprites[landingIndex];
                    break;
                }
        }
    }

    public void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    public void UpdateDragonSprites(DragonStatus status)
    {
        if(status == dragonStatus)
        {
            return;
        }

        dragonStatus = status;
        switch(dragonStatus)
        {
            case DragonStatus.NORMAL:
                {
                    idleSprites = new List<Sprite>(normalIdleSprites);
                    movementSprites = new List<Sprite>(normalMovementSprites);
                    break;
                }
            case DragonStatus.FAT:
                {
                    idleSprites = new List<Sprite>(fatIdleSprites);
                    movementSprites = new List<Sprite>(fatMovementSprites);
                    break;
                }
            case DragonStatus.SKINNY:
                {
                    idleSprites = new List<Sprite>(skinnyIdleSprites);
                    movementSprites = new List<Sprite>(skinnyMovementSprites);
                    break;
                }
        }
        UpdateAnimation(animationState);
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
