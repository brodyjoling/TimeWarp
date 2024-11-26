using UnityEngine;

public class GravityOff : MonoBehaviour
{
    private static Rigidbody rb;

    private float timePassed = 0;

    public static bool gravityOn = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gravityOn)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed >= 3)
        {
            turnGravityOn();
        }
    }
    public static void turnGravityOn()
    {
        GravityOff[] objectsWithRandomGravity = Object.FindObjectsByType<GravityOff>(FindObjectsSortMode.None);

        foreach (GravityOff objectWithRandomGravity in objectsWithRandomGravity)
        {
            objectWithRandomGravity.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
