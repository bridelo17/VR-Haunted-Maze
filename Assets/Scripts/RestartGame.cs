using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class RestartGame : MonoBehaviour

{
    public Transform vrRig;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public Button RetryButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    Time.timeScale = 1f;
    initialPosition = vrRig.position;
    initialRotation = vrRig.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
    initialPosition = vrRig.position;
    initialRotation = vrRig.rotation; 
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      
    }
}
