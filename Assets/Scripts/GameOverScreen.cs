using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{   

    private GameManager gameManager;

    // Awake is called before Start
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    public void RestartGame()
    {
        gameManager.RestartGame();
    }

    public void QuitGame()
    {
        gameManager.QuitGame();
    }
}
