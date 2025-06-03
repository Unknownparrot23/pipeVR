using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
[RequireComponent(typeof(Interactable))]
public class RestoreHierarchy : MonoBehaviour
{
    
    public Transform originalParent;
    void Start()
    {
        originalParent = transform.parent;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        hand.HoverUnlock(null);
        Invoke(nameof(ResetObject), 0.1f);
    }
    public void ResetObject()
    {
        transform.SetParent(originalParent);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
        }
    }
}
