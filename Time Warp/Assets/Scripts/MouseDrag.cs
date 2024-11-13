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
    public GameObject chip;
    //public Rigidbody thetwo;
    public Rigidbody intro;

    public GameObject insertChipText;

    public Texture insertedTexture;
    public MeshRenderer brokenChip;

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
        if (isDragging)
        {
            if (nearStation)
            {
                insertChipText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    insertChipText.SetActive(false);
                    chip.SetActive(false);
                    brokenChip.material.mainTexture = insertedTexture;
                    MoveToStation();
                    InsertDevice();
                }
            }
            else
            {
                insertChipText.SetActive(false);
            }
        }
        else
        {
            insertChipText.SetActive(false);
        }
        /*if (isDragging && nearStation && Input.GetKeyDown(KeyCode.E)) // make this into more if statements
        {
            chip.SetActive(false);
            brokenChip.material.mainTexture = insertedTexture;
            MoveToStation();
            InsertDevice();
        }*/
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
            if (rb == intro)
            {
                Vector3 paperRotation = Camera.main.transform.rotation.eulerAngles;
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);

                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                Vector3 direction = (objPosition - transform.position);


                rb.linearVelocity = direction * dragSpeed;

                paperRotation.x -= 90;
                rb.transform.rotation = Quaternion.Euler(paperRotation);
            }
            else
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera);

                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                Vector3 direction = (objPosition - transform.position);

                rb.linearVelocity = direction * dragSpeed;
            }
            
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
