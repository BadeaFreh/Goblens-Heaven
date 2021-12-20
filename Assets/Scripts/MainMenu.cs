using UnityEngine;
using UnityEngine.SceneManagement; // using SceneManagement for starting the game (Start Button)

public class MainMenu : MonoBehaviour
{
    public string firstLevel; // storing here the name of the scene as string


    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit(); // quitting the game
        Debug.Log("Quitting Game");
    }

}
