using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
[RequireComponent(typeof(Interactable))]
public class RestoreHierarchy : MonoBehaviour
{
    
    public Transform originalParent;
    // Start is called before the first frame update
    void Start()
    {
        // Store original values
        originalParent = transform.parent;
    }
    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnDetachedFromHand(Hand hand)
    {
        // Let default detachment happen first
        hand.HoverUnlock(null);
        Invoke(nameof(ResetObject), 0.1f); // Small delay to ensure physics settles
    }
    public void ResetObject()
    {
        // Detach from hand
        print("your here");
        transform.SetParent(originalParent);


        // If using rigidbody, reset physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
        }
    }
}
