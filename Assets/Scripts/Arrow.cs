using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    Image arrowImage;
    RectTransform imageTransform;

    public Transform start;
    public Transform target;

    public float borderSize = 20;

    private Camera mainCamera;

    public bool displayArrow = false;

    public bool TargetInScreen()
    {
        // if the object is in camera frame

        var planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Vector3 targetPosition = target.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(targetPosition) < 0)
            {
                return false;
            }
        }

        return true;
    }


    void Start()
    {
        arrowImage = GetComponent<Image>();
        imageTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(displayArrow)
        {
            if (TargetInScreen())
            {
                arrowImage.enabled = false;
            }
            else
            {
                arrowImage.enabled = true;

                imageTransform.up = (target.position - start.position).normalized;
                imageTransform.Rotate(new Vector3(0, 0, 90));

                Vector3 targetPositionOnScreen = mainCamera.WorldToScreenPoint(target.position);
                targetPositionOnScreen.x = Mathf.Clamp(targetPositionOnScreen.x, 0 + borderSize, Screen.width - borderSize);
                targetPositionOnScreen.y = Mathf.Clamp(targetPositionOnScreen.y, 0 + borderSize, Screen.height - borderSize);

                imageTransform.position = targetPositionOnScreen;
            }
        }
        else
        {
            arrowImage.enabled = false;
        }
    }
}
