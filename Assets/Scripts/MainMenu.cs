using UnityEngine;
using UnityEngine.SceneManagement; // using SceneManagement for starting the game (Start Button)

public class MainMenu : MonoBehaviour
{
    public string firstLevel; // storing here the name of the scene as string


    // there is a function for each button
    public void PlayGame() // function for the Play button
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void QuitGame() // function for the Quit button
    {
        Application.Quit(); // quitting the game
        Debug.Log("Quitting Game");
    }

}
