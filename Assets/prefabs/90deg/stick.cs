using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(Interactable))]




public class stick : MonoBehaviour
{
    
    public GameObject StartObject ;
    public Vector3 DisFromAxis ;
    public Vector3 otstup;



    private void OnDetachedFromHand(Hand hand)
    {
        // Let default detachment happen first
        hand.HoverUnlock(null);
        Invoke(nameof(SnapToPosition), 0.2f); // Small delay to ensure physics settles and give a chance to regrab 
    }


    private void SnapToPosition()
    {

        //RotationToPoint ROTcomponent = StartObject.GetComponent<RotationToPoint>();
        //if (ROTcomponent != null)
        //{
        //    Debug.Log(ROTcomponent.fromMid);
        //    Debug.Log(ROTcomponent.Radius);
        //    //float lenght = ROTcomponent.fromMid.magnitude;
        //    //Vector3 newPos = ROTcomponent.fromMid * ROTcomponent.Radius / lenght;
        //    //transform.localPosition = newPos;
        //}
        RotationToPoint ROTcomponent = StartObject.GetComponent<RotationToPoint>();
        float lenght = DisFromAxis.magnitude;
        Vector3 newPos = DisFromAxis * ROTcomponent.Radius / lenght;
        transform.localPosition = newPos+ (otstup * ROTcomponent.Radius);
    }


}
