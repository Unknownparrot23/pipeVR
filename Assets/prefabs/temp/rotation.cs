using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class rotationToPoint11 : MonoBehaviour
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

    //private void OnDetachedFromHand(Hand hand)
    //{
    //    // Let default detachment happen first
    //    hand.HoverUnlock(null);
    //    Invoke(nameof(SnapToPosition), 0.2f); // Small delay to ensure physics settles and give a chance to regrab 
    //}


    //private void SnapToPosition()
    //{
    //    float lenght = fromMid.magnitude;
    //    Vector3 newPos =fromMid*Radius/lenght;
    //    target.localPosition = newPos;

    //}




    void Update()
    {
        // Calculate direction to target
        Vector3 directionToTarget = target.position - transform.position;
        Vector3 fromMid = directionToTarget - Vector3.Project(directionToTarget, transform.right);
        Debug.Log("Found vector: "+fromMid);
        Vector3 fromMidNORM = fromMid;
        fromMidNORM.Normalize();

        Quaternion targetRotation = Quaternion.Euler(180f, 90, -Vector3.SignedAngle(fromMidNORM, transform.forward, transform.right));//Quaternion.LookRotation(fromMidNORM,Vector3.up);//     

        this.transform.GetChild(0).transform.rotation = targetRotation;// child 




    }
}
