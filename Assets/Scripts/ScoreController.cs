using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public GameObject bestScoreText;
    public GameObject flynetText;
    public GameObject clouds;
    private GameObject arSession;

    private int score = 0;
    private int numCaughtDragonflys = 0;
    private int bestScore = 0;
    private float spawnTimer = 5f;
    private int dragonflyShootThreshold = 3;

    private void Start()
    {
        arSession = GameObject.FindGameObjectsWithTag("ARSession")[0];
    }

    public void IncreaseScore()
    {
        gameObject.GetComponent<Text>().text = "Kills: " + (++score).ToString();
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.GetComponent<Text>().text = "Best: " + bestScore.ToString();
        }
    }

    public void IncreaseNumCaughtDragonflys()
    {
        numCaughtDragonflys++;
        if (numCaughtDragonflys == dragonflyShootThreshold)
        {
            arSession.GetComponent<ContentController>().spawnSpiders(false);
            numCaughtDragonflys = 0;
            StartCoroutine(KillSpiders());
        }
        flynetText.GetComponent<Text>().text = numCaughtDragonflys.ToString() + "/" + dragonflyShootThreshold.ToString();
    }

    public IEnumerator KillSpiders()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(clouds);
        GameObject[] spiders = GameObject.FindGameObjectsWithTag("Spider");

        foreach (GameObject spider in spiders)
        {
            StartCoroutine(spider.GetComponent<SpiderController>().Kill());
        }

        yield return new WaitForSeconds(3);
        arSession.GetComponent<ContentController>().spawnSpiders(true);
    }

    public void ResetScore()
    {
        score = 0;
        spawnTimer = 5f;
        gameObject.GetComponent<Text>().text = "Kills: " + score.ToString();
    }

    public float getSpawnTimer()
    {
        return spawnTimer;
    }

    public void setSpawnTimer(float value)
    {
        spawnTimer = value;
    }
}
