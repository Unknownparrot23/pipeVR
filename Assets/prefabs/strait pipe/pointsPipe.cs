using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



// Adjusts the model's length via scale so it connects two points. The modification is intentionally done only relative to the local x-coordinate  
// so that when connected, the pipe always aligns with the normal of the attachment. The adjustment of the ends relative to the model is done using the Aline script.  


//����������� ����� ������ ����� scale ����� ��� ��������� ��� �����. ��������� ���������� ��� ������ ������������ ��������� ���������� x
//����� ��� ������������� ����� ������ ������������� �� ������� � ���������. ���������� ������ ������������ ������ ������������� � ������� ������� Aline.

public class pointto : MonoBehaviour
{
    public GameObject ending;
    public GameObject modelToConnect;


    [Header("standard")]
    [SerializeField] public float PipeLength=0.7f;

    void Start()
    {
        
    }


    void Update()
    {
        Vector3 start = transform.position;
        Vector3 end = ending.transform.position;
        Vector3 ConnectingVector= start -end ;
        Quaternion rotOfunitVector = transform.rotation;
        Vector3 unitVector = new Vector3(1, 0, 0);
        unitVector = rotOfunitVector * unitVector;

        float Scale = (Vector3.Dot(ConnectingVector, unitVector))/ PipeLength;

        Vector3 antiparentscale = transform.localScale;// ��� ��� ������ �������� ������� ������� �� ������� ���� �������� ������ � ������ �������  
        antiparentscale.x = 1 / antiparentscale.x;                     
        antiparentscale.y = Scale / antiparentscale.y;
        antiparentscale.z = 1/ antiparentscale.z;                        
        modelToConnect.transform.localScale=antiparentscale; 

    }
}
