using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
    int playerLifePoints;
    public GameObject ScoreText;

    void Start()
    {
        playerLifePoints = 10;
    }

    public void IncreaseDamage()
    {
        // var tempColor = gameObject.GetComponent<Image>().color;
        // tempColor.a += .05f;
        // gameObject.GetComponent<Image>().color = tempColor;
        playerLifePoints--;

        if (playerLifePoints <= 0)
        {
            playerLifePoints = 10;
            // tempColor = gameObject.GetComponent<Image>().color;
            // tempColor.a = 0f;
            // gameObject.GetComponent<Image>().color = tempColor;
            ScoreText.GetComponent<ScoreController>().ResetScore();
        }
    }
}
