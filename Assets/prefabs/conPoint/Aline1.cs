using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Aline : MonoBehaviour
{
    [Header("local cords to snap")]
    [SerializeField] public bool UseXCord = false;
    [SerializeField] public float Xcord = 0;
    [SerializeField] public bool UseYCord = false;
    [SerializeField] public float Ycord = 0;
    [SerializeField] public bool UseZCord = false;
    [SerializeField] public float Zcord = 0;

    private void OnDetachedFromHand(Hand hand)
    {
        // Let default detachment happen first
        hand.HoverUnlock(null);
        Invoke(nameof(SnapToPosition), 0.21f); // Small delay to ensure physics settles and give a chance to regrab 
    }
  

    private void SnapToPosition()
    {
        Vector3 tempPosition = transform.localPosition;
        if (UseXCord) tempPosition.x = Xcord;
        if (UseYCord) tempPosition.y = Ycord;
        if (UseZCord) tempPosition.z = Zcord;
        transform.localPosition = tempPosition;
    }
}
