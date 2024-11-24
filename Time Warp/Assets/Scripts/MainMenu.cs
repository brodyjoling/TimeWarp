using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public AudioSource startSound;
    public void startGame()
    {
        startSound.Play();
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Tut");
    }

}
