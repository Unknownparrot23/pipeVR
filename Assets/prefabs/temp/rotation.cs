using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtTargetWithTwoFixedAxes : MonoBehaviour
{
    [Tooltip("The target object to point at")]
    public Transform target;

    [Tooltip("Primary axis to keep fixed (world space)")]
    public Vector3 fixedPrimaryAxis = Vector3.up;

    [Tooltip("Secondary axis to keep fixed (world space)")]
    public Vector3 fixedSecondaryAxis = Vector3.right;

    [Tooltip("Rotation speed (0 for instant)")]
    public float rotationSpeed = 0f;

    void Update()
    {
        if (target == null) return;

        // Calculate direction to target
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.Normalize();
        fixedPrimaryAxis = transform.up;
        fixedSecondaryAxis = transform.forward;

        // Calculate the constrained forward direction
        // We'll keep two axes fixed, so the third must be perpendicular to both
        Vector3 constrainedForward = directionToTarget;

        // Remove components along the fixed axes
        constrainedForward -= Vector3.Project(constrainedForward, fixedPrimaryAxis);
        constrainedForward -= Vector3.Project(constrainedForward, fixedSecondaryAxis);
        constrainedForward.Normalize();

        // Calculate the constrained up vector (just use primary fixed axis)
        Vector3 constrainedUp = fixedPrimaryAxis;

        // Calculate target rotation
        Quaternion targetRotation;
        if (constrainedForward != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(constrainedForward, constrainedUp);
        }
        else
        {
            // Fallback if we're looking directly along one of the fixed axes
            targetRotation = Quaternion.LookRotation(transform.forward, constrainedUp);
        }

        // Apply rotation
        if (rotationSpeed > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }
}
