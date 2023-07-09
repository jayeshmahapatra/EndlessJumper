using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float spawnThreshold = 1f;
    public GameObject player;

    [Header("UI Attributes")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;


    private Camera mainCamera;
    private PlatformSpawnManager platformSpawnManager;

    private float gameDuration = 0f;
    private bool isGameOver = false;

    [Header("Menu Attributes")]
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject HUD;

    private float gameOverDelay = 1f;

    // Camera Audio Source
    private AudioSource cameraAudioSource;

    // Awake is called before Start
    void Awake()
    {

        // UnPause the game
        Time.timeScale = 0f;

        // Show the start screen
        startScreen.SetActive(true);

        // Get the PlatformSpawnManager component
        platformSpawnManager = GetComponent<PlatformSpawnManager>();

        // Set the main camera
        mainCamera = Camera.main;

        // Get the camera's audio source
        cameraAudioSource = mainCamera.GetComponent<AudioSource>();

    }

    void Start()
    {
        // Start the background music
        cameraAudioSource.Play();
    }
    

    // Update is called once per frame
    void Update()
    {   

        if (isGameOver)
            return;

        float cameraBottom = mainCamera.transform.position.y - mainCamera.orthographicSize;
        float spawnYThreshold = cameraBottom - spawnThreshold;

        platformSpawnManager.UpdatePlatforms(moveSpeed, spawnYThreshold);

        gameDuration += Time.deltaTime;
        scoreText.text = "Score : " + Mathf.Round(gameDuration);


        // Check if the player goes off-screen
        if (player.transform.position.y < spawnYThreshold)
        {
            EndGame();
        }

    }

    // Start the game
    public void StartGame()
    {
        // Hide the start screen
        startScreen.SetActive(false);

        // Unpause the game
        Time.timeScale = 1f;

        // Set score to 0
        gameDuration = 0f;
        
        // Spawn platforms
        platformSpawnManager.SpawnPlatforms();

        // Set the HUD to active
        HUD.SetActive(true);

        // Set Player to active
        player.SetActive(true);
    }

    // End the game if the player goes off-screen
    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("Game Over! Score: " + Mathf.Round(gameDuration));
        // You can perform other game over actions here, like showing a game over screen or restarting the game.

        StartCoroutine(DelayedGameOver());
        
    }

    public void RestartGame()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {   
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    IEnumerator DelayedGameOver()
    {
        // Wait for 3 seconds
        yield return new WaitForSecondsRealtime(gameOverDelay);

        // Pause the game
        Time.timeScale = 0f;

        // Set the game over screen to active
        gameOverScreen.SetActive(true);

        // Set the HUD to inactive
        HUD.SetActive(false);

        // Set the final score text
        finalScoreText.text = "Score : " + Mathf.Round(gameDuration);

        
    }

}
