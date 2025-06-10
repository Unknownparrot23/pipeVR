using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pre rotates a model to connect


public class perRot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(180, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
