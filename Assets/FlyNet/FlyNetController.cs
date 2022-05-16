using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNetController : MonoBehaviour
{
    public GameObject scoreText;
    private Animation animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animation>();
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void CatchDragonfly()
    {
        if (!animator.isPlaying)
        {
            transform.localScale = new Vector3(.3f, .3f, .3f);
            StartCoroutine(Catch());
        }
    }

    public IEnumerator Catch()
    {
        animator.Play("catch");

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100000f))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit.TryGetComponent(out DragonflyController dragonflyController))
            {
                StartCoroutine(dragonflyController.Catch());
                yield return new WaitForSeconds(.5f);
                scoreText.GetComponent<ScoreController>().IncreaseNumCaughtDragonflys();
            }
        }
        yield return new WaitForSeconds(.5f);
        transform.localScale = new Vector3(0, 0, 0);
    }
}
