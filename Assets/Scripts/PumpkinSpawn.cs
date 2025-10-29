using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawn : MonoBehaviour
{
    [Header("Assign the Endless GameObject (or leave null to auto-find by tag)")]
    public GameObject Endless;

    [Header("Assign spawn point transforms in the Inspector")]
    public List<Transform> pumpkinSpawnPoints = new List<Transform>();

    [Header("Pumpkin prefab to spawn")]
    public GameObject pumpkinPrefab;

    [Header("Initial pumpkins to spawn at start")]
    public int pumpkinCount = 5;

    [Header("Time between new pumpkin spawns (Endless mode)")]
    public float spawnInterval = 5f;

    private Coroutine spawnRoutine;

    // Track which spawn points currently have pumpkins
    private Dictionary<Transform, GameObject> activePumpkins = new Dictionary<Transform, GameObject>();

    void Awake()
    {
        if (Endless == null)
            Endless = GameObject.FindWithTag("Endless");
    }

    void Start()
    {
        if (pumpkinSpawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        // Spawn initial pumpkins
        SpawnInitialPumpkins();

        // Start endless spawning if the mode is active
        if (Endless != null && Endless.activeSelf)
        {
            spawnRoutine = StartCoroutine(SpawnPumpkinsContinuously());
        }
    }

    void Update()
    {
        if (Endless != null)
        {
            if (Endless.activeSelf && spawnRoutine == null)
            {
                spawnRoutine = StartCoroutine(SpawnPumpkinsContinuously());
            }
            else if (!Endless.activeSelf && spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
        }
    }

    void SpawnInitialPumpkins()
    {
        List<Transform> shuffledPoints = new List<Transform>(pumpkinSpawnPoints);
        for (int i = 0; i < shuffledPoints.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledPoints.Count);
            (shuffledPoints[i], shuffledPoints[randomIndex]) = (shuffledPoints[randomIndex], shuffledPoints[i]);
        }

        for (int i = 0; i < Mathf.Min(pumpkinCount, shuffledPoints.Count); i++)
        {
            SpawnPumpkinAt(shuffledPoints[i]);
        }
    }

    IEnumerator SpawnPumpkinsContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (pumpkinSpawnPoints.Count == 0)
                yield break;

            // Clean up inactive or destroyed pumpkins
            List<Transform> freePoints = new List<Transform>();
            foreach (var point in pumpkinSpawnPoints)
            {
                if (!activePumpkins.ContainsKey(point) || activePumpkins[point] == null || !activePumpkins[point].activeInHierarchy)
                {
                    freePoints.Add(point);
                    if (activePumpkins.ContainsKey(point))
                        activePumpkins.Remove(point);
                }
            }

            if (freePoints.Count == 0)
                continue; // All points occupied, skip this cycle

            // Spawn at a random free point
            Transform randomPoint = freePoints[Random.Range(0, freePoints.Count)];
            SpawnPumpkinAt(randomPoint);
        }
    }

    void SpawnPumpkinAt(Transform spawnPoint)
    {
        if (activePumpkins.ContainsKey(spawnPoint))
            return; // Skip if a pumpkin already exists here

        GameObject pumpkin = Instantiate(pumpkinPrefab, spawnPoint.position, spawnPoint.rotation);
        activePumpkins[spawnPoint] = pumpkin;

        // Optional: if pumpkin has a script with OnDestroyed event
        Pumpkin pumpkinScript = pumpkin.GetComponent<Pumpkin>();
        if (pumpkinScript != null)
        {
            pumpkinScript.OnDestroyed += () =>
            {
                if (activePumpkins.ContainsKey(spawnPoint))
                    activePumpkins.Remove(spawnPoint);
            };
        }
    }
}
