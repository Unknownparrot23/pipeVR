using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EverythingIsHeld : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnAttachedToHand(Hand hand)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject conpoint = transform.GetChild(i).gameObject;
            HeldObjectTrigger component = conpoint.GetComponent<HeldObjectTrigger>();
            component.isHeld = true;
            component.hasTriggered = false;
            component.holdingHand = hand;
        }
    }

    //public void EisHeld()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        GameObject conpoint= transform.GetChild(i).gameObject;
    //        HeldObjectTrigger component = conpoint.GetComponent<HeldObjectTrigger>();
    //        component.isHeld = true;
    //        component.hasTriggered = false;
    //    }
    //}
    public void OnDetachFromHand(Hand hand)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject conpoint = transform.GetChild(i).gameObject;
            HeldObjectTrigger component = conpoint.GetComponent<HeldObjectTrigger>();
            component.isHeld = false;
            component.hasTriggered = true;
            component.holdingHand = null;
        }
    }
}
