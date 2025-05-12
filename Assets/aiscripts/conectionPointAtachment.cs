using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class conectionPointAtachment : MonoBehaviour
{
    [Header("SteamVR Interactable Settings")]
    public SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public SteamVR_Input_Sources handType;

    [Header("Collision Settings")]
    public string targetTag = "TargetObject"; // Tag of objects we want to trigger with
    public LayerMask collisionLayers = ~0; // Default to all layers

    private bool isBeingHeld = false;
    private bool hasCollidedWhileHeld = false;
    private GameObject lastCollidedObject;

    // Event that can be subscribed to from other scripts
/////////////   // public event Action<GameObject> OnTriggeredRelease;

    private SteamVR_Behaviour_Pose handPose;
    private Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<Interactable>();
        }
    }

    private void OnEnable()
    {
 //////////////       interactable.onAttachedToHand += OnPickup;
 //////////////       interactable.onDetachedFromHand += OnRelease;
    }

    private void OnDisable()
    {
 //////////////       interactable.onAttachedToHand -= OnPickup;
 //////////////////       interactable.onDetachedFromHand -= OnRelease;
    }

    private void OnPickup(SteamVR_Behaviour_Pose pose, SteamVR_Input_Sources source)
    {
        isBeingHeld = true;
        hasCollidedWhileHeld = false;
        handPose = pose;
        handType = source;
    }

    private void OnRelease(SteamVR_Behaviour_Pose pose, SteamVR_Input_Sources source)
    {
        if (hasCollidedWhileHeld && lastCollidedObject != null)
        {
            // Trigger the function
           //////////////////// OnTriggeredRelease?.Invoke(lastCollidedObject);

            // You can also call a specific method directly:
            // TriggerFunction(lastCollidedObject);
        }

        isBeingHeld = false;
        hasCollidedWhileHeld = false;
        lastCollidedObject = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isBeingHeld) return;

        // Check if collision is with the right layer and tag
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0 &&
            (string.IsNullOrEmpty(targetTag) || collision.gameObject.CompareTag(targetTag)))
        {
            hasCollidedWhileHeld = true;
            lastCollidedObject = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isBeingHeld) return;

        if (collision.gameObject == lastCollidedObject)
        {
            hasCollidedWhileHeld = false;
            lastCollidedObject = null;
        }
    }

    // Example function that can be called when conditions are met
    private void TriggerFunction(GameObject otherObject)
    {
        Debug.Log($"Triggered with {otherObject.name} after release!");

        // Add your custom functionality here
        // For example:
        // otherObject.GetComponent<Renderer>().material.color = Color.green;
        // Destroy(otherObject);
        // etc.
    }
}