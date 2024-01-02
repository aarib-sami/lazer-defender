using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] public bool alive = true;

    private void Awake()
    {
        SetUpSinglton();
    }

    private void SetUpSinglton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
 
    
    public void DecreaseHealth(int damageValue)
    {
        FindObjectOfType<Player>().health -= damageValue;
    }


}
