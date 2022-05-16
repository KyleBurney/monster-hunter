using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour
{
    public GameObject Spider;
    public GameObject ScoreText;
    private Animation animation;

    private Vector3 startEulerAngles;
    private Vector3 startGyroAttitudeToEuler;

    //Animator animator;

    void Start()
    {
        Input.gyro.enabled = true;
        //animator = gameObject.GetComponent<Animator>();
        animation = gameObject.GetComponent<Animation>();
    }
    
    /*void Update()
    {
        bool pull = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButton(0);

        bool release = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            || Input.GetMouseButtonUp(0);

        if (pull)
        {
            Vector3 point = Input.touchCount > 0
               ? Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, transform.position.z))
               : Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); // get world coordinates of mouse position
            
            if (!animation.isPlaying && 
                ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
                || Input.GetMouseButtonDown(0)))
            {
                animation.Play("pull");
            }
        }
        else if (release)
        {
            StartCoroutine(Shoot());
        }
    }*/

    public void Pull()
    {
        Vector3 point = Input.touchCount > 0
            ? Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, transform.position.z))
            : Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); // get world coordinates of mouse position

        if (!animation.isPlaying &&
                ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                || Input.GetMouseButtonDown(0)))
        {
            animation.Play("pull");
        }
    }

    public void Release()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        animation.Play("release");
        yield return new WaitForSeconds(.5f);
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100000f))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit.TryGetComponent(out SpiderController spiderController))
            {
                spiderController.Hit();
                ScoreText.GetComponent<ScoreController>().IncreaseScore();
            }
        }
    }

    private bool CanShoot()
    {
        return !animation.isPlaying && 
            ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0));
    }
}
