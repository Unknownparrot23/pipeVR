using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


[RequireComponent(typeof(Interactable))]
public class aline : MonoBehaviour
{
    [Header("local cords to snap")]
    [SerializeField] public bool UseXCord = false;
    [SerializeField] public float Xcord = 0;
    [SerializeField] public bool UseYCord = false;
    [SerializeField] public float Ycord = 0;
    [SerializeField] public bool UseZCord = false;
    [SerializeField] public float Zcord = 0;
    private void OnDetachedFromHand(Hand hand)
    {
        Vector3 tempPosition = new Vector3(0f,0f,0f);
        if (UseXCord == true) { tempPosition.x = Xcord; };
        if (UseYCord == true) { tempPosition.y = Ycord; };
        if (UseZCord == true) { tempPosition.z = Zcord; };
        transform.localPosition = tempPosition;
    }
}
