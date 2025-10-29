using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject player;
    public GameObject EndlessModeObject;
    void Awake()
    {
    DontDestroyOnLoad(EndlessModeObject);
    }

    void Start()
    {
    EndlessModeObject.SetActive(false);
    }

    public void BeginGame()
    {
    Destroy(player);    
    SceneManager.LoadScene("Haunted Maze",LoadSceneMode.Single);

    }

    // Update is called once per frame
    public void ResetGame()
    {
    Destroy(player);
    Time.timeScale = 1f;
    SceneManager.LoadScene("Menu",LoadSceneMode.Single);  
     
    }
    public void EndlessMode()
    {
    Destroy(player);
    EndlessModeObject.SetActive(true);
    SceneManager.LoadScene("Haunted Maze",LoadSceneMode.Single);
    } 

}
