using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
   public void LoadGame()
    {
       SceneManager.LoadScene("LaserDefender");
        FindObjectOfType<GameSession>().ResetGame();
    }

    
   public void LoadGameOver()
    {
        StartCoroutine(loadGame());
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }


    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator loadGame()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("GameOver");
    }


}
