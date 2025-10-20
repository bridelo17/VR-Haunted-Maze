using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
     
    public TextMeshProUGUI countText;
    private int count;

    void Start()
    {
    count = 0;
    SetCountText();   
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
    }

    void Update()
    {
        
    }
}
