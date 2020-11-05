using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private int sceneIndex;
    public void Quit()
    {
        Debug.Log("Aplication quit!");
        Application.Quit();
        //Application.OpenURL("google.com");
    }

    public void Retry()
    {
        Debug.Log("Aplication Retry!");
        
        if (sceneIndex > 0 && sceneIndex <3 )
        {
            SceneManager.LoadScene(sceneIndex + 1);

        }
        else
        {
            SceneManager.LoadScene(0);

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
