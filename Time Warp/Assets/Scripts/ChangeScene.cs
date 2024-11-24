using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{

    private static ChangeScene instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name.Equals("Level ThreeA"))
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync("Level ThreeB");
            }
            else if (currentScene.name.Equals("Level ThreeB"))
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync("Level ThreeA");
            }
        }
    }
}
