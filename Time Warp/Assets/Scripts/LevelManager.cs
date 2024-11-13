using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private string[] levels;

    public ScreenWipe screenWipe;
    private int nextLevelIndex;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //screenWipe = GetComponent<ScreenWipe>();
    }

    void Update()
    {
        
    }
    public void startNextLevel()
    {
        StartCoroutine(loadNextLevel());
    }

    private IEnumerator loadNextLevel()
    {
        nextLevelIndex++;
        if (nextLevelIndex >= levels.Length)
            nextLevelIndex = 0;

        string nextLevelName = levels[nextLevelIndex];

        screenWipe.ToggleWipe(true);
        Debug.Log("screenWipe toggled to blocked");

        // Wait for screen wipe to complete blocking
        while (!screenWipe.isDone)
        {
            //Debug.Log("Waiting for screen to be blocked...");
            yield return null;
        }

        Debug.Log("screenWipe finished blocking");

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevelName);
        Debug.Log("SceneManager loading scene: " + nextLevelName);

        // Wait for the scene to load
        while (!operation.isDone)
        {
            Debug.Log("Waiting for scene to load...");
            yield return null;
        }

        Debug.Log("Scene load complete, unblocking screen");

        screenWipe.ToggleWipe(false);
        Debug.Log("screenWipe toggled to not blocked");

        // Wait for screen wipe to complete 

        Debug.Log("screenWipe finished unblocking");
    }
}
