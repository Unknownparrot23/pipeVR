using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;
using static AttachmentGraphManager;

public class EverythingIsHeld : MonoBehaviour
{
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

        AttachmentGraphManager graph = FindObjectOfType<AttachmentGraphManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            graph.DisconnectObjects(gameObject, transform.GetChild(i).gameObject.name);
        }
    }
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
