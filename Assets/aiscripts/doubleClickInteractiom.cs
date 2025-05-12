using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class doubleClickInteractiom : MonoBehaviour { 

public float doubleClickTime = 0.3f;
private float lastClickTime;
private int clickCount = 0;
private Interactable interactable;
public GameObject Menu;
public GameObject PlayerObj;
private Vector3 spawnPos=new Vector3(0.4f,0.2f,0);



    //public SteamVR_Action_Pose cameraRotation = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose", "Head");

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (interactable.isHovering)
        {
            // Check which hands are hovering
            foreach (var hand in interactable.hoveringHands)
            {
                if (hand.GetGrabStarting() != GrabTypes.None)
                {
                    if (Time.time - lastClickTime < doubleClickTime)
                    {
                        // Double click detected
                        OnDoubleClick(hand);
                        clickCount = 0;
                    }
                    else
                    {
                        // First click
                        clickCount = 1;
                    }
                    lastClickTime = Time.time;


                }
                print(lastClickTime);
            };
            }
    }
    






public void OnDoubleClick(Hand hand)
{
        Vector3 playerDir = transform.position - PlayerObj.transform.position;
        spawnPos = playerDir.normalized;
        spawnPos = Quaternion.AngleAxis(90, Vector3.up) * spawnPos*0.5f;
        Menu.transform.position = transform.position + spawnPos;
        Menu.transform.rotation = Quaternion.LookRotation(playerDir);
        Menu.gameObject.SetActive(true);
        Data component = Menu.GetComponent<Data>();
        if (component != null)
        {
            component.pointOfCreation = gameObject;
        }
        //hmdRotation);
        //Quaternion hmdRotation = cameraRotation.localRotation;
    }




}
