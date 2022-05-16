using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentController : MonoBehaviour
{
    public GameObject Spider;
    public GameObject Dragonfly;
    public GameObject ScoreText;
    private ScoreController scoreController;
    private int levelCountdown = 3;
    private bool shouldSpawnSpiders = true;

    void Start()
    {
        scoreController = ScoreText.GetComponent<ScoreController>();
        Instantiate(Spider);
        StartCoroutine(SpiderSpawnTimer());
        StartCoroutine(DragonflySpawnTimer());
    }

    public void spawnSpiders(bool shouldSpawn)
    {
        shouldSpawnSpiders = shouldSpawn;
    }

    private IEnumerator SpiderSpawnTimer()
    {
        while (true)
        {
            if (shouldSpawnSpiders)
            {
                yield return new WaitForSeconds(scoreController.getSpawnTimer());
                if (levelCountdown-- == 0)
                {
                    levelCountdown = 3;
                    if (scoreController.getSpawnTimer() < 5f)
                    {
                        scoreController.setSpawnTimer(scoreController.getSpawnTimer() - 1f);
                    }
                    else if (scoreController.getSpawnTimer() < 4f)
                    {
                        scoreController.setSpawnTimer(scoreController.getSpawnTimer() - 0.5f);
                    }
                    else if (scoreController.getSpawnTimer() < 2f)
                    {
                        scoreController.setSpawnTimer(scoreController.getSpawnTimer() - 0.2f);
                    }
                    else if (scoreController.getSpawnTimer() > 1f)
                    {
                        scoreController.setSpawnTimer(scoreController.getSpawnTimer() - 0.1f);
                    }
                }

                if (GameObject.FindGameObjectsWithTag("Spider").Length < 4)
                {
                    Instantiate(Spider);
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private IEnumerator DragonflySpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (GameObject.FindGameObjectsWithTag("Dragonfly").Length == 0)
            {
                Instantiate(Dragonfly);
            }
        }
    }
}
