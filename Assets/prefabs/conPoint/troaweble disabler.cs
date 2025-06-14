using UnityEngine;
using Valve.VR.InteractionSystem;

// Basic Throwable component that ensures proper Rigidbody setup when released

    [RequireComponent(typeof(Interactable))]
public class NonPhysicalThrowable : Throwable
{
    public Transform originalParent;

    void Start()
    {
        originalParent = transform.parent;
    }
    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);

        // After release, make it static
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        hand.HoverUnlock(null);
        Invoke(nameof(ResetObject), 0.1f);
    }
    public void ResetObject()
    {
        transform.SetParent(originalParent);
    }
}
