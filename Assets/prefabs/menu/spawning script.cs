using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
//A simple script that subscribes to a button and spawns a prefab.
// простой скрипт который подписываться на кнопку и спавнит префаб
public class Spawner : MonoBehaviour
{
    [Header("What to spawn")]
    [SerializeField] public GameObject objectToSpawn; 
    public Transform spawnLocation;
    [SerializeField] public Button spawnButton;
    void Start()
    {
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnObjectFromMenu);
        }
        else
        {
            Debug.LogWarning("No spawn button assigned to PrefabSpawner script!");
        }
    }
    public void SpawnObjectFromMenu()
    {
        Transform spawnLocation = GetComponent<Data>().pointOfCreation.transform;
        if (objectToSpawn != null)
        {
            if (spawnLocation != null)
            {
                GameObject spawndObject = Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
                Hand hand= FindObjectOfType<Hand>();
                hand.AttachObject(spawndObject, GrabTypes.Grip, Valve.VR.InteractionSystem.Hand.AttachmentFlags.ParentToHand);
                hand.DetachObject(gameObject);
                this.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("no selected points by menu");
                this.gameObject.SetActive(false);
            }
        }
    }
}
