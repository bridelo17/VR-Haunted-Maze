using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public GameObject Ghost;
    public Transform spawnpoint;
    public Button RetryButton;
    public AudioSource Music;
    public AudioClip ItemPickup;
    public AudioClip LoseLife;
    public AudioClip GameOver;
    public AudioClip WinGame;
    public AudioClip ChocolatePickup;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject MainCamera;
    public List<GameObject> objectsToCheck = new List<GameObject>();
    public float lives = 1;
    private int count;
    public float GhostDistance;
    public float damageCooldown = 2f;
    private float lastDamageTime = -999f;

    void Start()
    {
    Time.timeScale = 1f;
    RetryButton.gameObject.SetActive(false);    
    count = 0;
    SetCountText();  
    winTextObject.SetActive(false);
    loseTextObject.SetActive(false); 
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
        if (count==1)
        {
            GameObject newGhost = Instantiate(Ghost,spawnpoint.position,spawnpoint.rotation);
            objectsToCheck.Add(newGhost);
        }
        if (count==2)
        {
            GameObject newGhost = Instantiate(Ghost,spawnpoint.position,spawnpoint.rotation);
            objectsToCheck.Add(newGhost);
        }
        if (count==3)
        {
            GameObject newGhost = Instantiate(Ghost,spawnpoint.position,spawnpoint.rotation);
            objectsToCheck.Add(newGhost);
        }
        if (count>=4)
        {
            Music.Stop();
            Time.timeScale = 0f; // freeze game
            winTextObject.SetActive(true);
            AudioSource.PlayClipAtPoint(WinGame,transform.position,0.25f);
            RetryButton.gameObject.SetActive(true);
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
                AudioSource.PlayClipAtPoint(LoseLife,transform.position,0.5f);
                SetCountText();
            }
        
        }
    if (lives <=0)
        {
            Time.timeScale = 0f; // freeze game
            Music.Stop();
            loseTextObject.SetActive(true);
            AudioSource.PlayClipAtPoint(GameOver,transform.position,0.05f);
            RetryButton.gameObject.SetActive(true);
        }
    }

    }

   
}
