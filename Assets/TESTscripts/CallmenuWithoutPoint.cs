using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CallmenuWithoutPoint : MonoBehaviour
{
    [Header("SteamVR Input")]
    public SteamVR_Input_Sources handType; // Which controller (LeftHand or RightHand)
    public SteamVR_Action_Boolean grabAction; // The grab action (usually Grip)

    [Header("Settings")]
    public float checkRadius = 0.1f; // Radius to check for grabbable objects
    public LayerMask grabbableLayers; // Layers that contain grabbable objects

    private bool wasGrabPressed = false;
    public float doubleClickTime = 0.3f;
    private float lastClickTime;
    private int clickCount = 0;
    private Interactable interactable;
    public GameObject Menu;
    public GameObject PlayerObj;
    private Vector3 spawnPos = new Vector3(0.4f, 0.2f, 0);


    private void Start()
    {
        interactable = GetComponent<Interactable>();
        // Find by exact name
        PlayerObj = GameObject.Find("Player");

        if (PlayerObj == null)
        {
            Debug.LogError("Player object not found in scene!");
        }
        else
        {
            Debug.Log("Found player: " + PlayerObj.name);
            if ((PlayerObj.transform.Find("SteamVRObjects")).gameObject.activeInHierarchy)
            {
                PlayerObj = PlayerObj.transform.Find("SteamVRObjects").Find("VRCamera").gameObject;
            }
            else
            {
                PlayerObj = PlayerObj.transform.Find("NoSteamVRFallbackObjects").Find("FallbackObjects").gameObject;
            }
        }
        Menu = GameObject.Find("Menu");
        if (Menu == null)
        {
            Debug.LogError("Player object not found in scene!");
        }
        else
        {
            Debug.Log("Found player: " + Menu.name);
        }
    }

    void Update()
    {
        // Check if grab button is pressed this frame
        bool isGrabPressed = grabAction.GetState(handType);

        // Detect when grab button is first pressed
        if (isGrabPressed && !wasGrabPressed)
        {
            CheckForFailedGrab();
        }

        wasGrabPressed = isGrabPressed;
    }

    void CheckForFailedGrab()
    {
        // Check for nearby grabbable objects
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, grabbableLayers);

        // If no objects found, call the failed grab function
        if (hitColliders.Length == 0)
        {
            OnFailedGrabAttempt();
        }
    }

    void OnFailedGrabAttempt()
    {
        Debug.Log("Grab attempted but no object nearby on " + handType);
        Vector3 playerDir = transform.position - PlayerObj.transform.position;
        spawnPos = playerDir.normalized;
        spawnPos = Quaternion.AngleAxis(90, Vector3.up) * spawnPos * 0.8f;
        Menu.transform.position = transform.position + spawnPos;
        Menu.transform.rotation = Quaternion.LookRotation(playerDir);
        GameObject visibleMenu = Menu.transform.GetChild(0).gameObject;
        visibleMenu.gameObject.SetActive(true);
        Data component = visibleMenu.GetComponent<Data>();
        if (component != null)
        {
            component.pointOfCreation = gameObject;
        }
    }

    // Visualize the check radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
