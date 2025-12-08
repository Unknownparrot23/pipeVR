using UnityEngine;

public class TimedDestructionTarget : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 0.2f;

    private float currentHitTime = 0f;
    private bool isBeingHit = false;

    void Update()
    {
        if (isBeingHit)
        {
            currentHitTime += Time.deltaTime;

            if (currentHitTime >= timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Reset timer if not being hit
            currentHitTime = Mathf.Max(0f, currentHitTime - Time.deltaTime * 2f);
        }

        // Reset flag for next frame
        isBeingHit = false;
    }

    public void RegisterHit()
    {
        isBeingHit = true;
    }
}