using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour
{
    private GameObject WebScreen;
    private GameObject DamageScreen;
    private Animator animator;
    private Coroutine shootCoroutine;
    private int spiderHealth = 1;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        WebScreen = GameObject.Find("WebScreen");
        DamageScreen = GameObject.Find("DamageScreen");
        animator = gameObject.GetComponent<Animator>();
        SetPosition();
        shootCoroutine = StartCoroutine(ShootTimer());
    }

    public void Hit()
    {
        if(--spiderHealth <= 0)
        {
            StartCoroutine(Kill());
        }
    }

    public IEnumerator Kill()
    {
        Destroy(gameObject.GetComponent<BoxCollider>());
        StopCoroutine(shootCoroutine);
        SetWebAlpha(0f);
        yield return new WaitForSeconds(0.25f);
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1f);
        /*gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        SetPosition();
        gameObject.SetActive(true);
        shootCoroutine = StartCoroutine(ShootTimer());*/
        Destroy(gameObject);
    }

    private void SetWebAlpha(float alpha)
    {
        var tempColor = WebScreen.GetComponent<Image>().color;
        tempColor.a = alpha;
        WebScreen.GetComponent<Image>().color = tempColor;
    }

    private void SetPosition()
    {
        gameObject.transform.SetPositionAndRotation(new Vector3(GetRandomPosition(2, 8, true), GetRandomPosition(2, 8, true), GetRandomPosition(8, 30, false)), Quaternion.Euler(0, 0, 0));
        gameObject.transform.LookAt(Camera.main.transform);
    }

    private IEnumerator ShootTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        int age = 0;

        while(true)
        {
            yield return wait;
            age += 1;
            animator.SetTrigger("charge");
            yield return new WaitForSeconds(2.2f);
            SetWebAlpha(0.2f);
            yield return new WaitForSeconds(0.15f);
            SetWebAlpha(0f);
            DamageScreen.GetComponent<DamageController>().IncreaseDamage();

            if(age > 2)
            {
                Destroy(gameObject);
            }
        }
    }

    private int GetRandomPosition(int min, int max, bool canBeNegative)
    {
        int result = Random.Range(min, max);
        if (canBeNegative && Random.Range(0, 2).Equals(0))
        {
            result *= -1;
        }

        return result;
    }
}
