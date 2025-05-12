using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pointto : MonoBehaviour
{
    public GameObject ending;
    public GameObject modelToConnect;


    [Header("standard")]
    [SerializeField] public float PipeLength=0.7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 start = transform.position;
        Vector3 end = ending.transform.position;
        Vector3 ConnectingVector= start -end ;
        Quaternion rotOfunitVector = transform.rotation;
        Vector3 unitVector = new Vector3(1, 0, 0);
        unitVector = rotOfunitVector * unitVector;
        //print(ConnectingVector);
        float Scale = (Vector3.Dot(ConnectingVector, unitVector))/ PipeLength;
        //float Scale = ConnectingVector.x / PipeLength;
        //print(Scalex);
        Vector3 antiparentscale = transform.localScale;
        antiparentscale.x = 1 / antiparentscale.x;                     
        antiparentscale.y = Scale / antiparentscale.y;
        antiparentscale.z = 1/ antiparentscale.z;                        
        modelToConnect.transform.localScale=antiparentscale; //= ConnectingVector.magnitude/PipeLength;

        // modelToConnect.transform.rotation = Quaternion.LookRotation(ConnectingVector)* Quaternion.LookRotation(new Vector3 (0f, 1f, 0f));      always point to point

    }
}
