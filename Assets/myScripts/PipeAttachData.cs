using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeAttachData : MonoBehaviour
{
    public AttachmentData[] attachmentPoints;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i <= transform.childCount ; i++)
            //child transform.GetChildCount())
        {
            attachmentPoints[i].connectedObject = transform.GetChild(i).gameObject;
            attachmentPoints[i].id = attachmentPoints[i].connectedObject.name;
            attachmentPoints[i].localPosition = attachmentPoints[i].connectedObject.transform.localPosition;
            attachmentPoints[i].localRotation = attachmentPoints[i].connectedObject.transform.localRotation;
        }
    }
    void Start()
    {
        // Convert array to dictionary for the manager
        Dictionary<string, AttachmentData> pointsDict = new Dictionary<string, AttachmentData>();
        foreach (var point in attachmentPoints)
        {
            pointsDict.Add(point.id, point);
        }

        FindObjectOfType<AttachmentGraphManager>().RegisterObject(gameObject, pointsDict);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
