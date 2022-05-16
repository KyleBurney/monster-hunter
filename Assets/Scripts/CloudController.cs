using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LightningTimer());
    }

    private IEnumerator LightningTimer()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);

        // first bolt set
        yield return new WaitForSeconds(.5f);
        transform.GetChild(0).gameObject.SetActive(true);

        // second bolt set
        yield return new WaitForSeconds(.75f);
        transform.GetChild(1).gameObject.SetActive(true);

        // first bolt unset
        yield return new WaitForSeconds(.5f);
        transform.GetChild(0).gameObject.SetActive(false);

        // third bolt set
        yield return new WaitForSeconds(.75f);
        transform.GetChild(2).gameObject.SetActive(true);

        // second bolt unset
        yield return new WaitForSeconds(.5f);
        transform.GetChild(1).gameObject.SetActive(false);

        // third bolt unset
        yield return new WaitForSeconds(.5f);
        transform.GetChild(2).gameObject.SetActive(false);

        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
