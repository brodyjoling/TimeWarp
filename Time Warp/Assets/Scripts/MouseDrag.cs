using UnityEngine;
using System.Collections.Generic;

public class MouseDrag : MonoBehaviour
{
    Rigidbody rb;
    private float distanceFromCamera;
    private float dragSpeed = 10f;
    private float range = 8f;
    public static bool isDragging = false; // possible change

    //private Rigidbody introPaper;
    //private GameObject introPaperObject;

    private bool isPaper = false;

    private RandomGravity randomGravityScript;
    private RandomGravityStart randomGravityStartScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //introPaperObject = GameObject.Find("Paper");
        //introPaper = introPaperObject.GetComponent<Rigidbody>();
        if (rb.transform.parent.name == "Paper")
        {
            isPaper = true;
        }
        UpdateDistanceFromCamera();
    }

    void Update()
    {

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
            if (isPaper)
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

}
