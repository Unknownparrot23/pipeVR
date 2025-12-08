using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WeldColisionDetect : MonoBehaviour
{
    private ParticleSystem Sparks;
    private GameObject particles;
    [SerializeField] private float checkDistance = 0.05f;
    [SerializeField] private LayerMask targetLayers;
    // Start is called before the first frame update
    void Awake()
    {
        particles = transform.GetChild(0).gameObject;
        Sparks = particles.GetComponent<ParticleSystem>();
        if (Sparks == null)
        {
            Sparks = gameObject.AddComponent<ParticleSystem>();
        }
        Sparks.Stop();

    }

    private void Start()
    {
        // Add these checks
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("No Collider attached to " + gameObject.name);
        }

        // Also check if the GameObject has a Rigidbody
        if (GetComponent<Rigidbody>() == null)
        {
            Debug.LogError("No Rigidbody attached to " + gameObject.name);
        }
    }

    void Update()
    {
        CheckFront();
    }

    void CheckFront()
    {
        RaycastHit hit;

        // Cast a ray forward from the object's position
        if (Physics.Raycast(particles.transform.position, -particles.transform.forward, out hit, checkDistance, targetLayers))
        {
            Debug.Log($"Object detected in front: {hit.collider.gameObject.name}");
            Debug.DrawRay(particles.transform.position, -particles.transform.forward * checkDistance, Color.red);
            
            // You can access the detected object
            GameObject detectedObject = hit.collider.gameObject;
            // Do something with detectedObject
            Sparks.Play();
            TimedDestructionTarget target = hit.collider.GetComponent<TimedDestructionTarget>();
            if (target != null)
            {
                target.RegisterHit();
            }

        }
        else
        {
            Debug.DrawRay(particles.transform.position, -particles.transform.forward * checkDistance, Color.green);
            Sparks.Stop();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colision " + this.name);
        Sparks.Stop();
    }
    private void OnCollisionExit(Collision collision)
    {
        Sparks.Stop();
    }
    // Update is called once per frame

}
