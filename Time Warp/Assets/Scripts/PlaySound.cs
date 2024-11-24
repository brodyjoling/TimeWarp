using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private float time = 0;
    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        time += Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (time > 5)
            sound.Play();
    }
}
