using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Restores the object's hierarchy to its pre-grab state.
// Consider removing this and just enabling the checkbox in the Throwable component instead.

//возращает иерархию до подн€ти€ обьекта.
//может быть надо убрать и поставить галку в компоненте Throwable 


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
    }
}
