using UnityEngine;
using UnityEngine.SceneManagement; // for loading a new scene
using System.Collections; // for using IEnumerator
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitAfterDying = 2f; // 2 seconds

    [HideInInspector]
    public bool levelEnding;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // hide cursor
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // enter pause screen when pressing escape
        {
            PauseUnpause();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo()); // to wait couple of seconds before reloading the scene again
    }

    // using COUROUTINE
    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);

        // when we die, this function is called from PlayerHealthController to restart game (load the scene again)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        // that means we are actually on paused ..
        if (UIController.instance.pauseScreen.activeInHierarchy) // if pauseScreen (image in UICanvas) is active
        {
            UIController.instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else // inactive
        {
            UIController.instance.pauseScreen.SetActive(true); // activating  object in the hierarchy
            Cursor.lockState = CursorLockMode.None; // now mouse is free to move around (not to the center anymore)
            Time.timeScale = 0f;

        }
    }
}
