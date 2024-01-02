using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    GameSession gameSession;


    void Start()
    {

        healthText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameSession>().alive == true)
        {
            healthText.text = FindObjectOfType<Player>().GetHealth().ToString();
        }
        else
        {
            healthText.text = 0.ToString();
        }
    }
}
