using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [HideInInspector]
    public bool isSetup = false;
    [HideInInspector]
    public Vector3 targetLocation;
    public float secToReachTarget = 0.3f;

    private float timer = 0f;

    private void FixedUpdate()
    {
        if (isSetup)
        {
            if (timer < 1)
            {
                timer += Time.deltaTime / secToReachTarget;
            }
            else
            {
                timer = 1;
            }

            if (Vector3.Distance(transform.position, targetLocation) < 0.3f)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetLocation, Mathf.SmoothStep(0, 1, timer));
            }
        }
    }
}
