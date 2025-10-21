using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
     
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public float life = 1;
    private int count;
    

    void Start()
    {
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
        if (count>=4)
        {
            winTextObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            
            life =- 1;
        }
        if (life <=0)
        {
            loseTextObject.SetActive(true);
        }
    }

}
