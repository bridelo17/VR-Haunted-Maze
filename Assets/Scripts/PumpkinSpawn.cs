using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawn : MonoBehaviour
{
    [Header("Assign spawn point transforms in the Inspector")]
    public List<Transform> pumpkinSpawnPoints = new List<Transform>();

    [Header("Pumpkin prefab to spawn")]
    public GameObject pumpkinPrefab;

    [Header("Number of pumpkins to spawn at start")]
    public int pumpkinCount = 5;

    void Start()
    {
        // Safety check
        if (pumpkinSpawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        // Shuffle the list to randomize order
        List<Transform> shuffledPoints = new List<Transform>(pumpkinSpawnPoints); // make a copy of original list
        for (int i = 0; i < shuffledPoints.Count; i++) // loop through each index in the list
        {
            int randomIndex = Random.Range(i, shuffledPoints.Count); // picks a random position between current and end of list
            (shuffledPoints[i], shuffledPoints[randomIndex]) = (shuffledPoints[randomIndex], shuffledPoints[i]); // swaps current element with randomly chosen element. Fisher Yates shuffle
        }

        // Spawn pumpkins at the first 'pumpkinCount' points
        for (int i = 0; i < Mathf.Min(pumpkinCount, shuffledPoints.Count); i++)
        {
            Transform spawnPoint = shuffledPoints[i];
            Instantiate(pumpkinPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
