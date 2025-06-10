using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;






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
        hasTriggered = false; // Reset trigger state when picked up
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

        if (!isHeld) return; // Only trigger when held

        // Check if both objects need to be triggers
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
            // Reset trigger state when leaving the collision
            // Useful if you want to allow multiple triggers per hold
            hasTriggered = false;
        }
    }

    // Example function that gets called - replace with your own
    private void TargetFunction(GameObject target, Hand holdingHand)
    {
        Debug.Log("Triggered with " + target.name + " while being held!");

        // Example: Change color of the target object
        Renderer targetRenderer = target.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetRenderer.material.color = new Color(
                Random.value,
                Random.value,
                Random.value
            );
        }
        RestoreHierarchy Restore = GetComponent<RestoreHierarchy>();
        if (onlyPoint) { holdingHand.DetachObject(gameObject); }
        else { holdingHand.DetachObject(transform.parent.gameObject); }
        FindObjectOfType<AttachmentGraphManager>().ConnectObjects(Restore.originalParent.GameObject(), transform.name,target.transform.parent.gameObject,     target.transform.name);

    }
}
