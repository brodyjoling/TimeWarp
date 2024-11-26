using UnityEngine;

public class GravityOff : MonoBehaviour
{
    private Rigidbody rb;
    private float timePassed = 0;
    private bool paperTouched = false;

    Vector3 startPos;
    private bool isPaper = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb.transform.parent.name == "Paper")
        {
            startPos = rb.transform.position;
        }
    }

    void Update()
    {
        if (isPaper && rb.transform.position != startPos) 
        {
            paperTouched = true;
        }

        if (paperTouched)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed >= 3)
        {
            rb.isKinematic = false;
        }
    }
}
