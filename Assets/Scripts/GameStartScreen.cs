using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScreen : MonoBehaviour
{   
    private GameManager gameManager;
    
    // Awake is called before Start
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }
}
