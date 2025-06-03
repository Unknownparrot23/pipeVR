using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

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

    private void OnDetachedFromHand(Hand hand)
    {
        // Let default detachment happen first
        hand.HoverUnlock(null);
        Invoke(nameof(SnapToPosition), 0.2f); // Small delay to ensure physics settles and give a chance to regrab 
    }


    private void SnapToPosition()
    {
        float lenght = fromMid.magnitude;
        Vector3 newPos =fromMid*Radius/lenght;
        target.localPosition = newPos;

    }

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


    //    void Update()
    //    {
    //        // Calculate direction to target
    //        Vector3 directionToTarget = target.position - transform.position;
    //        Vector3 fromMid = directionToTarget - Vector3.Project(directionToTarget, transform.right);

    //        Vector3 fromMidNORM = fromMid;
    //        fromMidNORM.Normalize();

    //        if(Vector3.SignedAngle(fromMidNORM, transform.forward, transform.right) != 0)
    //        {
    //        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x-Vector3.SignedAngle(fromMidNORM, transform.forward, transform.right), 0, 0);// transform.parent.rotation*
    //        this.transform.rotation = targetRotation;        
    //        }

    //// child 

    //        //Quaternion targetRotation = Quaternion.Euler(0f, 0, -Vector3.SignedAngle(fromMidNORM, transform.forward, transform.right));// transform.parent.rotation*

    //        //this.transform.GetChild(0).transform.rotation = targetRotation;// child 
    //        stick snaper = target.GetComponent<stick>();
    //        if (snaper != null)
    //        {
    //            snaper.DisFromAxis = fromMid;
    //            snaper.otstup =transform.right;
    //        }



}

