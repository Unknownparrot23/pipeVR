using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//When a prefab is added (this script is placed in an empty object), it records all attachment points and registers the object they belong to.
//Still a bit rough (untested) script that I wrote.

//когда добавляеться префаб(этот скрипт находиться в пустыщке) записывает все точки крепления и регестрирует обьект которому они пренадлежат 
//Ещё сыроватый(непровереный) скрипт который я написал. 


public class PipeAttachData : MonoBehaviour
{
    //void Awake()
    //{
    //    for (int i = 0; i <= transform.childCount ; i++)

    //    {
    //        attachmentPoints[i].connectedObject = transform.GetChild(i).gameObject;
    //        attachmentPoints[i].id = attachmentPoints[i].connectedObject.name;
    //        attachmentPoints[i].localPosition = attachmentPoints[i].connectedObject.transform.localPosition;
    //        attachmentPoints[i].localRotation = attachmentPoints[i].connectedObject.transform.localRotation;
    //    }
    //}
    public AttachmentData[] attachmentPoints;

    void Start()
    {
        attachmentPoints = new AttachmentData[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            attachmentPoints[i]= new AttachmentData();
            attachmentPoints[i].PointObject = transform.GetChild(i).gameObject;
            attachmentPoints[i].id = attachmentPoints[i].PointObject.name;
            attachmentPoints[i].localPosition = attachmentPoints[i].PointObject.transform.localPosition;
            attachmentPoints[i].localRotation = attachmentPoints[i].PointObject.transform.localRotation;
        }
        Dictionary<string, AttachmentData> pointsDict = new Dictionary<string, AttachmentData>();
        foreach (var point in attachmentPoints)
        {
            pointsDict.Add(point.id, point);
        }

        FindObjectOfType<AttachmentGraphManager>().RegisterObject(gameObject, pointsDict);
    }


    void Update()
    {
        
    }
}
