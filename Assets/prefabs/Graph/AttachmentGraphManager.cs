using System.Collections.Generic;
using UnityEngine;


//The  graph manager .

//созданный менеджер графа. 



public class AttachmentGraphManager : MonoBehaviour
{
    // Class to represent an attachment point
    public class AttachmentPoint
    {

        // rewrite for game objects
        public string id; // Unique identifier for this attachment point
        public Vector3 localPosition; // Position relative to parent object
        public Quaternion localRotation; // Rotation relative to parent object
        public GameObject connectedObject; // What object is attached here
        public string connectedPointId; // Which point on the other object we're connected to
        public GameObject PointObject;
        public AttachmentPoint(string id,Vector3 localPosition, Quaternion localRotation, GameObject PointObject)
        {
            this.id = id;
            this.PointObject = PointObject; // What object is attached here
            this.localPosition = localPosition;
            this.localRotation = localRotation;
        }
        public void Refresh()
        {
            this.localPosition = PointObject.transform.localPosition;
            this.localRotation = PointObject.transform.localRotation;
        }
    }

    // Class to represent an object in our graph
    public class AttachableObject
    {
        public GameObject obj;
        public Dictionary<string, AttachmentPoint> attachmentPoints = new Dictionary<string, AttachmentPoint>();
        public List<AttachableObject> connectedObjects = new List<AttachableObject>();

        public AttachableObject(GameObject obj)
        {
            this.obj = obj;
        }

        public void AddAttachmentPoint(string id, Vector3 localPosition, Quaternion localRotation, GameObject PointObject)
        {
            if (!attachmentPoints.ContainsKey(id))
            {
                attachmentPoints.Add(id, new AttachmentPoint(id, localPosition, localRotation, PointObject));
            }
        }

        public bool IsAttachmentPointFree(string pointId)
        {
            Debug.Log("contains "+attachmentPoints.ContainsKey(pointId)+" presoeden "+ attachmentPoints[pointId].connectedObject);
            return attachmentPoints.ContainsKey(pointId) &&
                   attachmentPoints[pointId].connectedObject == null;
        }
    }

    // Main graph storage
    public Dictionary<GameObject, AttachableObject> objectGraph = new Dictionary<GameObject, AttachableObject>();          //when it runs?

    // Register a new object with its attachment points
    public void RegisterObject(GameObject obj, Dictionary<string, AttachmentData> attachmentPoints)
    {
        if (!objectGraph.ContainsKey(obj))
        {
            AttachableObject newObject = new AttachableObject(obj);
            objectGraph.Add(obj, newObject);

            foreach (var point in attachmentPoints)
            {
                newObject.AddAttachmentPoint(point.Key, point.Value.localPosition, point.Value.localRotation,point.Value.PointObject);
            }

            Debug.Log($"Registered object {obj.name} with {attachmentPoints.Count} attachment points");
        }
    }

    // Connect two objects at specific attachment points
    public bool ConnectObjects(GameObject obj1, string pointId1, GameObject obj2, string pointId2)
    {
        foreach (GameObject key in objectGraph.Keys)
        {
            Debug.Log("Key: " + key.GetInstanceID());
        }
        Debug.Log("ConnectObjects is called obj1: "+ !objectGraph.ContainsKey(obj1)+ " name: " + obj1.name + "  " + obj1.GetInstanceID()+ "  obj2: " + !objectGraph.ContainsKey(obj2)+" name: "+obj2.name+"  "+ obj2.GetInstanceID());
        if (!objectGraph.ContainsKey(obj1) || !objectGraph.ContainsKey(obj2))
        {
            Debug.LogWarning("One or both objects not registered");
            return false;
        }

        AttachableObject aObj1 = objectGraph[obj1];
        AttachableObject aObj2 = objectGraph[obj2];

        if (!aObj1.attachmentPoints.ContainsKey(pointId1) || !aObj2.attachmentPoints.ContainsKey(pointId2))
        {
            Debug.LogWarning("One or both attachment points don't exist");
            return false;
        }

        if (!aObj1.IsAttachmentPointFree(pointId1) || !aObj2.IsAttachmentPointFree(pointId2))
        {
            Debug.LogWarning("One or both attachment points are already in use");
            return false;
        }

        // Make the connection
        aObj1.attachmentPoints[pointId1].connectedObject = obj2;
        aObj1.attachmentPoints[pointId1].connectedPointId = pointId2;

        aObj2.attachmentPoints[pointId2].connectedObject = obj1;
        aObj2.attachmentPoints[pointId2].connectedPointId = pointId1;

        // Add to connected objects list
        if (!aObj1.connectedObjects.Contains(aObj2))
            aObj1.connectedObjects.Add(aObj2);

        if (!aObj2.connectedObjects.Contains(aObj1))
            aObj2.connectedObjects.Add(aObj1);

        // Position the objects correctly
        PositionConnectedObjects(obj1, pointId1, obj2, pointId2);

        Debug.Log($"Connected {obj1.name} at {pointId1} to {obj2.name} at {pointId2}");
        return true;
    }

    // Position two connected objects so their attachment points align
    private void PositionConnectedObjects(GameObject obj2, string pointId2, GameObject obj1, string pointId1)
    {
        AttachableObject aObj1 = objectGraph[obj1];
        AttachableObject aObj2 = objectGraph[obj2];

        // Get world positions and rotations of attachment points
        Vector3 worldPos1 = obj1.transform.TransformPoint(aObj1.attachmentPoints[pointId1].localPosition);
        Quaternion worldRot1 = obj1.transform.rotation * aObj1.attachmentPoints[pointId1].localRotation;

        Vector3 worldPos2 = obj2.transform.TransformPoint(aObj2.attachmentPoints[pointId2].localPosition);
        Quaternion worldRot2 = obj2.transform.rotation * aObj2.attachmentPoints[pointId2].localRotation;

        // Calculate the rotation that makes obj2's X-axis opposite to obj1's X-axis
        Quaternion flipXRotation = Quaternion.FromToRotation(worldRot2 * Vector3.right, worldRot1 * -Vector3.right);

        // Apply the rotation to obj2
        obj2.transform.rotation = flipXRotation * obj2.transform.rotation;

        // Position obj2 so the attachment points meet
        Vector3 offset = worldPos1 - obj2.transform.TransformPoint(aObj2.attachmentPoints[pointId2].localPosition);
        obj2.transform.position += offset;
    }

    // Disconnect two objects
    public void DisconnectObjects(GameObject obj1, string pointId1)
    {
        if (!objectGraph.ContainsKey(obj1) || !objectGraph[obj1].attachmentPoints.ContainsKey(pointId1))
            return;

        AttachmentPoint point = objectGraph[obj1].attachmentPoints[pointId1];
        if (point.connectedObject == null)
            return;

        GameObject obj2 = point.connectedObject;
        string pointId2 = point.connectedPointId;

        // Clear connection references
        point.connectedObject = null;
        point.connectedPointId = null;

        objectGraph[obj2].attachmentPoints[pointId2].connectedObject = null;
        objectGraph[obj2].attachmentPoints[pointId2].connectedPointId = null;

        // Remove from connected objects list if no other connections exist
        if (GetConnectionCount(obj1, obj2) == 0)
        {
            objectGraph[obj1].connectedObjects.Remove(objectGraph[obj2]);
            objectGraph[obj2].connectedObjects.Remove(objectGraph[obj1]);
        }

        Debug.Log($"Disconnected {obj1.name} at {pointId1} from {obj2.name} at {pointId2}");
    }

    // Helper to count how many connections exist between two objects
    private int GetConnectionCount(GameObject obj1, GameObject obj2)
    {
        int count = 0;
        foreach (var point in objectGraph[obj1].attachmentPoints.Values)
        {
            if (point.connectedObject == obj2)
                count++;
        }
        return count;
    }

    // Get all objects connected to a specific object
    public List<GameObject> GetConnectedObjects(GameObject obj)
    {
        List<GameObject> result = new List<GameObject>();
        if (objectGraph.ContainsKey(obj))
        {
            foreach (var connectedObj in objectGraph[obj].connectedObjects)
            {
                result.Add(connectedObj.obj);
            }
        }
        return result;
    }

    // Get all attachment points for an object
    public Dictionary<string, AttachmentPoint> GetAttachmentPoints(GameObject obj)
    {
        if (objectGraph.ContainsKey(obj))
        {
            return objectGraph[obj].attachmentPoints;
        }
        return new Dictionary<string, AttachmentPoint>();
    }
}

// Helper struct to define attachment points in the Inspector
[System.Serializable]
public struct AttachmentData
{
    public string id;
    public Vector3 localPosition;
    public Quaternion localRotation;
    public GameObject PointObject;
}