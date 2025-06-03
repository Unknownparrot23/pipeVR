using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class doubleClickInteractiom : MonoBehaviour
{
    public float doubleClickTime = 0.3f;
    private float lastClickTime;
    private int clickCount = 0;
    private Interactable interactable;
    public GameObject Menu;
    public GameObject PlayerObj;
    private Vector3 spawnPos = new Vector3(0.4f, 0.2f, 0);



    void Awake()
    {
        // Ensure the Interactable component is properly configured
        var interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<Valve.VR.InteractionSystem.Interactable>();
        }

        // Other necessary SteamVR components
        var throwable = GetComponent<Valve.VR.InteractionSystem.Throwable>();
        if (throwable == null)
        {
            throwable = gameObject.AddComponent<Valve.VR.InteractionSystem.Throwable>();
        }
    }

    void OnEnable()
    {
        Valve.VR.InteractionSystem.Interactable interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        if (interactable != null)
        {
            //interactable.onAttachedToHand += OnAttachedToHand;
            //interactable.onDetachedFromHand += OnDetachedFromHand;
        }
    }

    void OnDisable()
    {
        Valve.VR.InteractionSystem.Interactable interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        if (interactable != null)
        {
            //interactable.onAttachedToHand -= OnAttachedToHand;
            //interactable.onDetachedFromHand -= OnDetachedFromHand;
        }
    }

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


    private void Update()
    {
        //if (interactable.isHovering)
        //{
        //    Debug.Log("Hovering");
        foreach (var hand in interactable.hoveringHands)
        {
            if (hand.GetGrabStarting() != GrabTypes.None)
            {
                if (Time.time - lastClickTime < doubleClickTime)
                {
                    Debug.Log("clickCount: " + clickCount);

                    OnDoubleClick(hand);
                    clickCount = 0;
                }
                else
                {
                    clickCount = 1;
                    Debug.Log("Hovering3");
                }
                lastClickTime = Time.time;
            }
            //        Debug.Log("Hovering2.2");
            //    };
            //    }
        }

    }







    public void OnDoubleClick(Hand hand)
    {
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




}
