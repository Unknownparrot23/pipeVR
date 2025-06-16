using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

// Rotates the attachment so that the model (child of the attachment) always connects two points.
// The rotation is performed around the local x-axis (right, Red).  
// Tracks the Target (the other end), which is set in the inspector.,  
// so that when it calls OnDetachedFromHand, it triggers its return to the end of the pipe.  
[RequireComponent(typeof(Interactable))]
public class RotationToPoint : MonoBehaviour
{
    [Tooltip("The target object to point at")]
    public Transform target;
    [Tooltip("Radius")]
    public float Radius = 0.075f;
    [SerializeField]public Vector3 fromMid;
    public GameObject ang90;

    void Update()
    {
        Vector3 localDirection = transform.parent.InverseTransformPoint(target.position) -
                                transform.parent.InverseTransformPoint(transform.position);
        Vector3 fromMid = localDirection - Vector3.Project(localDirection, Vector3.right);
        fromMid.Normalize();

        float angle = Vector3.SignedAngle(fromMid, Vector3.forward, Vector3.right);
        this.fromMid = fromMid;
        if (Mathf.Abs(angle) > 0.01f) 
        {
            Quaternion targetRotation;

            targetRotation = Quaternion.AngleAxis(-angle, Vector3.right);
            transform.localRotation = targetRotation ;
            target.transform.localRotation= targetRotation*Quaternion.Euler(0,-90,0);
        }
    }
    private void delayCoordinates()
    {
        
        float length = fromMid.magnitude;
        Vector3 newPos = fromMid * Radius / length;
        Debug.Log("fromMid "+fromMid+" Radius"+Radius+"newPos"+newPos);
        target.transform.localPosition = newPos - (Vector3.right * Radius);

    }
    public void CylindricalCoordinates()
    {
        Debug.Log("Detached");
        Invoke(nameof(delayCoordinates), 0.2f); // Small delay to ensure physics settles and give a chance to regrab 
    }


}

