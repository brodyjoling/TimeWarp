using UnityEngine;

public class LevelOneGravity : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 randomGravity;
    private float gravityStrength;
    private float changeTime = 5;
    private float timeSinceLastChange = 0f;
    private bool gravityReset = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ChangeGravity();
    }

    void Update()
    {
        if (!gravityReset)
        {
            if (timeSinceLastChange >= changeTime)
            {
                ChangeGravity();
                timeSinceLastChange = 0f;
                changeTime = Random.Range(1, 7);
            }
            //rb.AddForce(Vector3.up * 1);
            rb.AddForce(randomGravity, ForceMode.Acceleration);
        }
        else
        {
            rb.useGravity = true;
        }
        timeSinceLastChange += Time.fixedDeltaTime;
    }

    void ChangeGravity()
    {
        gravityStrength = Random.Range(0.01f, .02f);
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
