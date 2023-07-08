using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float spawnThreshold = 1f;
    public GameObject player;
    public TextMeshProUGUI scoreText;

    private Camera mainCamera;
    private PlatformSpawnManager platformSpawnManager;

    private float gameDuration = 0f;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        platformSpawnManager = GetComponent<PlatformSpawnManager>();
        platformSpawnManager.SpawnPlatforms();
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

    // End the game if the player goes off-screen
    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("Game Over! Score: " + Mathf.Round(gameDuration));
        // You can perform other game over actions here, like showing a game over screen or restarting the game.
    }

}
