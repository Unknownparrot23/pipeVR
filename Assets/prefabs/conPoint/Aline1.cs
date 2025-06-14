using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Replaces the local coordinate with an adjustable variable, which is set in the inspector when you release this object.  

// Заменяем локальную координату на переменную на указываю в окне когда ты отпускаешь этот объект


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
        // Small delay to ensure physics settles and give a chance to regrip
        Invoke(nameof(SnapToPosition), 0.21f);

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
