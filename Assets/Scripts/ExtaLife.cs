using UnityEngine;
using System.Collections.Generic;

public class ExtaLife : MonoBehaviour
{

    public List<Transform> chocolateSpawnPoints = new List<Transform>();
    public GameObject chocolatePrefab;
    public int chocolateCount = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        // Safety check
        if (chocolateSpawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }


        List<Transform> shuffledPoints = new List<Transform>(chocolateSpawnPoints); // make a copy of original list
        for (int i = 0; i < shuffledPoints.Count; i++) // loop through each index in the list
        {
            int randomIndex = Random.Range(i, shuffledPoints.Count); // picks a random position between current and end of list
            (shuffledPoints[i], shuffledPoints[randomIndex]) = (shuffledPoints[randomIndex], shuffledPoints[i]); // swaps current element with randomly chosen element. Fisher Yates shuffle
        }


        for (int i = 0; i < Mathf.Min(chocolateCount, shuffledPoints.Count); i++)
        {
            Transform spawnPoint = shuffledPoints[i];
            Instantiate(chocolatePrefab, spawnPoint.position, spawnPoint.rotation);
            // check if loop ran
        }


}
}

