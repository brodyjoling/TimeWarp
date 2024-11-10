using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    Rigidbody rb;
    private float distanceFromCamera;
    private float dragSpeed = 10f;
    private float range = 8f;
    private bool isDragging = false;
    private bool nearStation = false;

    private Vector3 stationPosition;
    private float moveSpeed = 5f;

    public Collider inputStationCollider;
    public Rigidbody theone;

    private RandomGravity randomGravityScript;
    private RandomGravityStart randomGravityStartScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stationPosition = inputStationCollider.transform.position;

        UpdateDistanceFromCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging && nearStation && Input.GetKeyDown(KeyCode.E)) // make this into more if statements
        {
            theone.isKinematic = true;
            MoveToStation();
            InsertDevice();
        }
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= range)
        {
            isDragging = true;
            UpdateDistanceFromCamera();
        }
    }
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera);

            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 direction = (objPosition - transform.position);

            rb.linearVelocity = direction * dragSpeed;
        }

    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void UpdateDistanceFromCamera()
    {
        Collider col = GetComponent<Collider>();
        float objectSize = col.bounds.extents.magnitude;
        distanceFromCamera = Mathf.Clamp(objectSize * 1.5f, 2f, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == inputStationCollider)
        {
            nearStation = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == inputStationCollider)
        {
            nearStation = false;
        }
    }

    private void InsertDevice()
    {
        ResetAllGravity();
        Debug.Log("Inserted...");
    }

    void MoveToStation()
    {
        transform.position = Vector3.MoveTowards(transform.position, stationPosition, moveSpeed * Time.deltaTime);
    }

    private void ResetAllGravity()
    {
        RandomGravity[] gravityObjects = FindObjectsOfType<RandomGravity>();

        foreach (RandomGravity gravityobject in gravityObjects)
        {
            gravityobject.ResetGravity();
        }
        Debug.Log("Gravity Stablized");


        RandomGravityStart[] gravityObjects1 = FindObjectsOfType<RandomGravityStart>();

        foreach (RandomGravityStart gravityobject1 in gravityObjects1)
        {
            gravityobject1.ResetGravity();
        }
        Debug.Log("Gravity Stablized");
    }

}
