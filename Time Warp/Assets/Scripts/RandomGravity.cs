using UnityEngine;

public class RandomGravity : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 randomGravity;
    private float gravityStrength;
    private float changeTime = Random.Range(3,10);
    private float timeSinceLastChange = 0f;
    private float floatStrength = Random.Range(0,12);

    private bool gravityReset = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeGravity();
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
        floatStrength = Random.Range(0, 12);
        gravityStrength = Random.Range(0.01f, 50f);
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
