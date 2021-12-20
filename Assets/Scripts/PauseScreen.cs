using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{

    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // each button in scene gets a function
    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene); // LoadScene takes a string
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
