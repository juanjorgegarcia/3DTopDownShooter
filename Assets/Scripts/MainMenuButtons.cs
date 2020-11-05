using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Quit()
    {
        Debug.Log("Aplication quit!");
        Application.Quit();
    }

    public void Play()
    {
        Debug.Log("Play game!");
        SceneManager.LoadScene("MainLevel1");
    }

}