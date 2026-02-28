using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // called by UI button to begin game
    public void OnGameStart()
    {
        // load the first scene (index 0) in Build Settings
        SceneManager.LoadScene(0);
    }
}
