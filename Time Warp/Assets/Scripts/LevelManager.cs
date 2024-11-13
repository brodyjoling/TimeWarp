using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private string[] levels;

    public ScreenWipe screenWipe;
    private int nextLevelIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //screenWipe = GetComponent<ScreenWipe>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(RandomGravityStart.gravityReset);
        if (RandomGravityStart.gravityReset)
        {
            Debug.Log("gravityReset in level manager is true");
            //implement wait for a couple seconds
            StartCoroutine(loadNextLevel());
        }
    }

    private IEnumerator loadNextLevel()
    {
        nextLevelIndex++;
        if (nextLevelIndex >= levels.Length) {
            nextLevelIndex = 0;
        }

        string nextLevelName = levels[nextLevelIndex];

        screenWipe.ToggleWipe(true);
        Debug.Log("screenWipe toggled");
        while (!screenWipe.isDone)
        {
            Debug.Log("screenWipe.isDone: " + screenWipe.isDone);

            yield return null;
        }
        Debug.Log("screenWipe finished");

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevelName);
        Debug.Log("SceneManager loading scene maybe");

        while (!operation.isDone)
        {
            yield return null;
        }

        screenWipe.ToggleWipe(false);
    }
}
