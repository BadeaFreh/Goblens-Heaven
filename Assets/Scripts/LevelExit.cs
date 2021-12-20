using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{

    public string nextLevel;
    public float waitToEndLevel; // small delay when triggering portal

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.levelEnding = true;
            // SceneManager.LoadScene(nextLevel);  // moved to Couroutine function
            StartCoroutine(EndLevelCo());
        }
    }

    private IEnumerator EndLevelCo()
    {

        yield return new WaitForSeconds(waitToEndLevel);

        SceneManager.LoadScene(nextLevel);

    }
}
