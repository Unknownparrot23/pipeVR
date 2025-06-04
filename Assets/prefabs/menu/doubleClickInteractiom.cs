using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

// Not tested in VR

// Displays menu on double grab. Awake and Enable methods were AI-generated and appear non-functional.

// Scene contains single menu instance that hides when inactive. On call, stores reference to triggering object.
// Core functionality handles menu positioning (distance-based offset and readable rotation)


// не проверен в вр 

// Вызов меню при двойном хватание объекта. Методы Awake и Еnable написаны AI и вроде не работают. 
// меню в сцене одно и скрываеться когда не нужно. При вызове сохраняеться относительно какого обьекта вызываеться меню
// основной текст функции это вызов меню с на растоянии от обьекта и с поворотом для чтения



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


    public void regeseraclick()
    {
                if (Time.time - lastClickTime < doubleClickTime)
                {
                    Debug.Log("clickCount: " + clickCount);

                    OnDoubleClick();
                    clickCount = 0;
                }
                else
                {
                    clickCount = 1;
                    Debug.Log("Hovering3");
                }
                lastClickTime = Time.time;
    }







    public void OnDoubleClick()
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
