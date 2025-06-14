using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


// Displays menu on double grab. Awake and Enable methods were AI-generated and appear non-functional.
// Scene contains single menu instance that hides when inactive. On call, stores reference to triggering object.
// Core functionality handles menu positioning (distance-based offset and readable rotation)
// Вызов меню при двойном хватание объекта. Методы Awake и Enable написаны AI и вроде не работают. 
// меню в сцене одно и скрывается когда не нужно. При вызове сохраняется относительно какого объекта вызывается меню
// основной текст функции это вызов меню с на расстоянии от объекта и с поворотом для чтения
public class doubleClickInteraction : MonoBehaviour
{
    public float doubleClickTime = 0.3f;
    private float lastClickTime;
    private int clickCount = 0;
    private Interactable interactable;
    public GameObject Menu;
    public GameObject PlayerObj;
    private Vector3 spawnPos = new Vector3(0.4f, 0.2f, 0);
    public void RegisterClick()
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
                }
                lastClickTime = Time.time;
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
