using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public GameObject Ghost;
    public List<Transform> ghostSpawnPoints = new List<Transform>();
    public Button RetryButton;
    public AudioSource Music;
    public AudioClip ItemPickup;
    public AudioClip LoseLife;
    public AudioClip GameOver;
    public AudioClip WinGame;
    public AudioClip ChocolatePickup;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject scoreTextObject;
    public GameObject MainCamera;
    public List<GameObject> objectsToCheck = new List<GameObject>();
    public GameObject Endless;
    public float lives = 1;
    private int count;
    private int lastcount = 0;
    public float GhostDistance;
    public float damageCooldown = 2f;
    private float lastDamageTime = -999f;
    private int score;

    void Start()
    {
    Endless = GameObject.FindWithTag("Endless");
    Time.timeScale = 1f;
    RetryButton.gameObject.SetActive(false);    
    count = 0;
    SetCountText();  
    winTextObject.SetActive(false);
    loseTextObject.SetActive(false); 
    scoreTextObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pumpkin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            AudioSource.PlayClipAtPoint(ItemPickup,transform.position,0.5f);
            SetCountText();
            Debug.Log("Pumpkin picked up");
        }
        if (other.gameObject.CompareTag("Chocolate"))
        {
        other.gameObject.SetActive(false);
        lives+=1;
        AudioSource.PlayClipAtPoint(ChocolatePickup,transform.position,0.5f);
        SetCountText();
        }
    
    }
   void SetCountText()
{
    countText.text = "Pumpkin Count: " + count.ToString();
    livesText.text = "Lives: " + lives.ToString();

    if (count > lastcount)
    {
        int ghostsToSpawn = count - lastcount;
        for (int i = 0; i < ghostsToSpawn; i++)
        {
            if (ghostSpawnPoints.Count > 0)
            {
                // Pick a random spawn point from the list
                int randomIndex = Random.Range(0, ghostSpawnPoints.Count);
                Transform spawnPoint = ghostSpawnPoints[randomIndex];

                // Spawn the ghost at that point
                GameObject newGhost = Instantiate(Ghost, spawnPoint.position, spawnPoint.rotation);
                objectsToCheck.Add(newGhost);
            }
            else
            {
                Debug.LogWarning("No ghost spawn points defined!");
            }
        }
        lastcount = count;
    }

    if (count >= 4) // win condition
    {
        if (Endless == null)
            Endless = GameObject.FindWithTag("Endless");

        if (Endless == null || !Endless.activeSelf)
        {
            Music.Stop();
            Time.timeScale = 0f; // freeze game
            winTextObject.SetActive(true);
            AudioSource.PlayClipAtPoint(WinGame, transform.position, 0.1f);
            RetryButton.gameObject.SetActive(true);
        }
    }
}

    void Update()
    {
    if (Time.timeScale == 0f)
    return;
    
    foreach (GameObject obj in objectsToCheck)
    {
    float distance = Vector2.Distance(new Vector2(MainCamera.transform.position.x,MainCamera.transform.position.z), new Vector2(obj.transform.position.x, obj.transform.position.z));
    
    if (distance <=GhostDistance)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                lives -= 1;
                lastDamageTime = Time.time;
                AudioSource.PlayClipAtPoint(LoseLife,transform.position,0.1f);
                SetCountText();
            }
        
        }
    if (lives <=0)
        {
            Time.timeScale = 0f; // freeze game
            Music.Stop();
            score = count;
            scoreText.text= "Score: " + score.ToString();
            scoreTextObject.SetActive(true);
            loseTextObject.SetActive(true);
            AudioSource.PlayClipAtPoint(GameOver,transform.position,0.05f);
            RetryButton.gameObject.SetActive(true);
        }
    }

    }
}

