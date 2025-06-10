using UnityEngine;
using Valve.VR.InteractionSystem;

// Basic Throwable component that ensures proper Rigidbody setup when released

// просто Throwable с удостоверением что когда отпускаешь Rigidbody правильно настроен
public class NonPhysicalThrowable : Throwable
{
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
    }
}
