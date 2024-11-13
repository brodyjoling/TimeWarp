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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RandomGravityStart.gravityReset)
        {
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

        //screenWipe.ToggleWipe(true);
        //while (!screenWipe.isDone)
        //{
       //     yield return null;
      //  }

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevelName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
