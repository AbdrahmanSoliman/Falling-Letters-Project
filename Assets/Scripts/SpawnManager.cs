using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject letterCube;
    private float startSpawn = 0f; // When to spawn the cube for first time
    private float spawnInterval = 0.5f; // Time between cube and another to be spawned
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCube", startSpawn, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCube()
    {
        if(!GameManager.isGameOver && GameManager.isGamePlayable) // spawn only when game is not over
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2, 2), 6, 0);
            Instantiate(letterCube, spawnPos, letterCube.transform.rotation);
        }
    }
}
