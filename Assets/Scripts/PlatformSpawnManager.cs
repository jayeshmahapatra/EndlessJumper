using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour
{
    public GameObject platformPrefab;
    
    public int platformCount = 10;
    public float minDistanceBetweenPlatforms = 2f;
    public float initialPlatformOffset = 2f;
    public float xRange = 5f;

    private GameObject player;
    private List<Transform> platforms = new List<Transform>();

    void Awake()
    {
        // Get player from the GameManager script
        player = GetComponent<GameManager>().player;

    }

    // Spawn platforms
    public void SpawnPlatforms()
    {
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.y -= initialPlatformOffset;

        for (int i = 0; i < platformCount; i++)
        {
            if (i == 0)
            {
                SpawnPlatform(spawnPosition);
            }
            else
            {
                spawnPosition.x = Random.Range(-xRange, xRange);
                SpawnPlatform(spawnPosition);
            }

            spawnPosition.y += minDistanceBetweenPlatforms;
        }
    }

    // Update platforms' positions
    public void UpdatePlatforms(float moveSpeed, float spawnYThreshold)
    {
        // Move platforms downwards
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            Transform platform = platforms[i];
            platform.position += Vector3.down * moveSpeed * Time.deltaTime;

            // Check if platform is below the spawn threshold, then reposition it above
            if (platform.position.y < spawnYThreshold)
            {
                RepositionPlatform(platform);
            }
        }
    }

    // Spawn a new platform
    private void SpawnPlatform(Vector3 spawnPosition)
    {
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platforms.Add(platform.transform);
    }

    // Reposition a platform above the highest platform
    private void RepositionPlatform(Transform platform)
    {
        float highestY = GetHighestPlatformY();
        Vector3 newPos = new Vector3(Random.Range(-xRange, xRange), highestY + Random.Range(minDistanceBetweenPlatforms, minDistanceBetweenPlatforms + 1.5f), 0f);
        platform.position = newPos;
        
    }

    // Get the highest Y position among existing platforms
    private float GetHighestPlatformY()
    {
        float highestY = float.MinValue;

        foreach (Transform platform in platforms)
        {
            if (platform.position.y > highestY)
                highestY = platform.position.y;
        }

        return highestY;
    }
}
