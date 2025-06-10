using UnityEngine;
using Valve.VR.InteractionSystem;
//a script that is added to the model and using it places a collider for triggering, but when clicked, it selects the entire object and not just the model

//скрипт, который добавляется к модели и с его помощью размещает коллайдер для срабатывания, но при щелчке он выбирает весь объект, а не только модель 
public class GrabParentInstead : MonoBehaviour
{
    public Transform parentToGrab; // Assign this in inspector to the parent you want to grab

    private void OnAttachedToHand(Hand hand)
    {
        // Detach this object from the hand
        hand.DetachObject(gameObject);

        // Attach the parent instead
        hand.AttachObject(parentToGrab.gameObject, GetBestGrabType(), Valve.VR.InteractionSystem.Hand.AttachmentFlags.ParentToHand );
    }

    private GrabTypes GetBestGrabType()
    {
        return GrabTypes.Grip; 
    }
}
