using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Restores the object's hierarchy to its pre-grab state.
// Consider removing this and just enabling the checkbox in the Throwable component instead.

//��������� �������� �� �������� �������.
//����� ���� ���� ������ � ��������� ����� � ���������� Throwable 


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
