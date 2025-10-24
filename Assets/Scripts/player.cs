using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public Button RetryButton;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject MainCamera;
    public List<GameObject> objectsToCheck = new List<GameObject>();
    public float lives = 1;
    private int count;
    public float GhostDistance;

    void Start()
    {
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
            SetCountText();
        }
    
    }
    void SetCountText()
    {
        countText.text = "Pumpkin Count: " + count.ToString();
        livesText.text = "Lives: " + lives.ToString();
        if (count>=4)
        {
            winTextObject.SetActive(true);
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
        lives = lives - 1;
        SetCountText();
        }
    if (lives <=0)
        {
            Time.timeScale = 0f; // freeze game
            loseTextObject.SetActive(true);
            RetryButton.gameObject.SetActive(true);
        }
    }

    }
    public void Restart()
    {
    Time.timeScale = 1f;    
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Debug.Log("Game restarted!");
    }
   
}
