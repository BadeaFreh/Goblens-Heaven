using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VectoryScreen : MonoBehaviour
{
    public string mainMenuScene;
    public float timeBetweenShowing = 1f;
    public GameObject textBox, returnButton; // references

    void Start()
    {
        StartCoroutine(ShowObjectsCo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // gets called when pressing the MainMenu Button (assigned in the inspector)
    public void MainMenu() // function for the Main Menu button
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public IEnumerator ShowObjectsCo()
    {
        // this line basically causes the waiting for 1 second before running the line after it
        yield return new WaitForSeconds(timeBetweenShowing);

        textBox.SetActive(true); // showing "congrats" message

        yield return new WaitForSeconds(timeBetweenShowing);

        returnButton.SetActive(true); // showing return button
    }
}
