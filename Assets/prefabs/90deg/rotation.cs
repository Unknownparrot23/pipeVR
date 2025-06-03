using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;



// Rotates the attachment so that the model (child of the attachment) always connects two points. The rotation is performed around the local x-axis (right, Red).  
// Tracks the Target (the other end), which is set in the inspector. The script also passes information to the Stick component,  
// so that when it calls OnDetachedFromHand, it triggers its return to the end of the pipe.  

// Possible improvement: Subscribe to OnDetachedFromHand. Then, this script could be disabled when grabbing the target,  
// and assigning it to both ends would make the pipe work in both directions.  

// Поворачивает крепление чтобы модель(ребёнок крепления) всегда соединял две точки. Поворот идёт вокруг локальной x (right, Red) оси
// Идёт слежка слежка за Target(другой конец) который устанавливается в инспекторе. Так же скрипт передаёт информацию компоненту stick.
// чтобы когда он вызывает метод OnDetachedFromHand он вызвал своё возращение к концу трубы.

//возможное улучшение подписываться на OnDetachedFromHand. тогда можно добавить что этот скрипт отключается когда ты берешь таргет и дать его обоим концам сделав трубу работающей в двух направлениях
[RequireComponent(typeof(Interactable))]
public class RotationToPoint : MonoBehaviour
{
    [Tooltip("The target object to point at")]
    public Transform target;

    [Tooltip("Primary axis to keep fixed (world space)")]
    public Vector3 fixedPrimaryAxis = Vector3.up;

    [Tooltip("Secondary axis to keep fixed (world space)")]
    public Vector3 fixedSecondaryAxis = Vector3.right;

    [Tooltip("Rotation speed (0 for instant)")]
    public float rotationSpeed = 0f;

    [Tooltip("Radius")]
    public float Radius = 0.078f;
    public Vector3 fromMid;
    public GameObject ang90; 

    void Update()
    {
        // Calculate direction in local space
        Vector3 localDirection = transform.parent.InverseTransformPoint(target.position) -
                                transform.parent.InverseTransformPoint(transform.position);

        // Project onto the plane defined by the right axis
        Vector3 fromMid = localDirection - Vector3.Project(localDirection, Vector3.right);
        fromMid.Normalize();

        float angle = Vector3.SignedAngle(fromMid, Vector3.forward, Vector3.right);

        if (Mathf.Abs(angle) > 0.01f) // Small threshold to prevent jitter
        {
            Quaternion targetRotation;

            if (rotationSpeed > 0)
            {
                targetRotation = Quaternion.AngleAxis(-angle, Vector3.right);
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                                         targetRotation,
                                                         rotationSpeed * Time.deltaTime);
            }
            else
            {
                targetRotation = Quaternion.AngleAxis(-angle, Vector3.right);
                transform.localRotation = targetRotation;
            }
        }

        // Update stick component if exists
        stick snaper = target.GetComponent<stick>();
        if (snaper != null)
        {
            snaper.DisFromAxis = fromMid;
            snaper.otstup = Vector3.right;
        }
    }




}

