using UnityEngine;

public class CrazyDoor : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddTorque(new Vector3(Random.Range(-5,5), 0, 0), ForceMode.Impulse);
    }
}
