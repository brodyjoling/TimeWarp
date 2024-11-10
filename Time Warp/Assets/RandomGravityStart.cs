using UnityEngine;

public class RandomGravityStart : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 randomGravity;
    private float gravityStrength;
    private float changeTime = 15;
    private float timeSinceLastChange = 0f;
    private float floatStrength = 0;

    private bool gravityReset = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gravityReset)
        {
            rb.AddForce(Vector3.up * floatStrength);
            rb.AddForce(randomGravity, ForceMode.Acceleration);
        }
        timeSinceLastChange += Time.fixedDeltaTime;

        if (timeSinceLastChange >= changeTime)
        {
            ChangeGravity();
            timeSinceLastChange = 0f;
            changeTime = Random.Range(1, 7);
        }

    }

    void ChangeGravity()
    {
        floatStrength = Random.Range(0, 15);
        gravityStrength = Random.Range(0.01f, 5f);
        randomGravity = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * gravityStrength;
    }

    public void ResetGravity()
    {
        gravityReset = true;

        rb.angularVelocity = Vector3.zero;
    }
}
