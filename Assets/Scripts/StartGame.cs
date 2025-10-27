using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject player;
    
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
}
