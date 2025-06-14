using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;
using static AttachmentGraphManager;
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class HeldObjectTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [Tooltip("Tag of objects that will trigger the function")]
    public string triggerTag = "ConectionPoint";

    [Tooltip("Should the function only be called once per hold?")]
    public bool triggerOnce = true;

    [Tooltip("Should both objects need to be triggers?")]
    public bool requireBothTriggers = true;

    public bool hasTriggered = false;
    public Hand holdingHand;
    public bool isHeld = false;
    public bool onlyPoint = false;

    private void OnAttachedToHand(Hand hand)
    {
        holdingHand = hand;
        isHeld = true;
        hasTriggered = false;
        onlyPoint = true;
    }
    private void OnDetachedFromHand(Hand hand)
    {
        isHeld = false;
        holdingHand = null;
        onlyPoint = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isHeld) return;
        if (requireBothTriggers)
        {
            if (!other.isTrigger || !GetComponent<Collider>().isTrigger) return;
        }

        if (other.CompareTag(triggerTag))
        {

            if (!triggerOnce || (triggerOnce && !hasTriggered))
            {
                TargetFunction(other.gameObject, holdingHand);
                hasTriggered = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            hasTriggered = false;
        }
    }
    private void TargetFunction(GameObject target, Hand holdingHand)
    {
        Debug.Log("Triggered with " + target.name + " while being held!");

        RestoreHierarchy Restore = GetComponent<RestoreHierarchy>();
        if (onlyPoint) {
            return; //Alt atach method
         }
        else { holdingHand.DetachObject(transform.parent.gameObject);
            AttachmentGraphManager graph =FindObjectOfType<AttachmentGraphManager>();
            foreach (KeyValuePair<GameObject, AttachableObject> pair in graph.objectGraph)
            {
                AttachableObject obj =pair.Value;
                foreach (KeyValuePair<string, AttachmentPoint> athPoints in obj.attachmentPoints)
                {
                    AttachmentPoint objpoint= athPoints.Value;
                    objpoint.Refresh();
                }
            }
        }
        FindObjectOfType<AttachmentGraphManager>().ConnectObjects(Restore.originalParent.GameObject(), 
                                                                    transform.name,
                                                                    target.transform.parent.gameObject,
                                                                    target.transform.name);

    }
}
