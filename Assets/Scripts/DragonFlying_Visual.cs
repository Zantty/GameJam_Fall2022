using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DragonFlying_Visual : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;
    private Vector3 initialPosition;
    private Vector3 initialFollowOffset;

    public float flyingHeight = 5;
    bool isFlying = false;

    public float transitionSpeed = 1;

    public void Set_FlyStatus(bool isFlying)
    {
        this.isFlying = isFlying;
    }


    private void Start()
    {
        initialPosition = transform.position;
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        initialFollowOffset = transposer.m_FollowOffset;
    }

    private void Update()
    {
        if(isFlying)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, initialPosition.z) - (Vector3.forward * flyingHeight);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * transitionSpeed);

            Vector3 newOffset = initialFollowOffset + (flyingHeight / 2) * Vector3.forward;
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, newOffset, Time.deltaTime * transitionSpeed);
        }
        else
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, initialPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * transitionSpeed);

            Vector3 newOffset = initialFollowOffset;
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, newOffset, Time.deltaTime * transitionSpeed);
        }
    }

}
