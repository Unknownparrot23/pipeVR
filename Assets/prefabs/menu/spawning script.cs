using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//A simple script that subscribes to a button and spawns a prefab.
//Here we need to modify the instance so that when the object is added, it's placed perpendicularly. I assumed all attachment points would show the X-axis along the normal.

// ������� ������ ������� ������������� �� ������ � ������� ������
// ��� ���� �������� instanse ��� ����� ������ ��� ������������ �������� ���������������. � ���������� ��� ��� ����� ��������� ����� ���������� x ���� �� �������

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
