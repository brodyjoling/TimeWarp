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

    private AudioSource sound;
    private bool soundPlayed = false;

    private bool isPaper = false;

    private bool isStabalizationDevice = false;
    //private MeshCollider mc;

    private AudioSource[] audioSources;

    private RandomGravity randomGravityScript;
    private RandomGravityStart randomGravityStartScript;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        //sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        //introPaperObject = GameObject.Find("Paper");
        //introPaper = introPaperObject.GetComponent<Rigidbody>();
        if (rb.transform.parent.name == "Paper")
        {
            isPaper = true;
        }
        if (rb.transform.parent.name == "StabalizationDevice")
        {
            isStabalizationDevice = true;
            //mc = GetComponent<MeshCollider>();
        }
        UpdateDistanceFromCamera();
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
                if(!soundPlayed)
                {
                    //sound.Play();
                    audioSources[0].Play();
                    soundPlayed = true;
                }
               
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
                if (!soundPlayed)
                {
                    audioSources[1].Play();
                    soundPlayed = true;
                }
                
                if (isStabalizationDevice)
                {
                    gameObject.layer = LayerMask.NameToLayer("NoCollision");
                    Debug.Log("layerChange");
                    //mc.isTrigger = true;
                }

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
        soundPlayed = false;
        if (isStabalizationDevice)
        {
            //mc.isTrigger = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void UpdateDistanceFromCamera()
    {
        Collider col = GetComponent<Collider>();
        float objectSize = col.bounds.extents.magnitude;
        distanceFromCamera = Mathf.Clamp(objectSize * 1.5f, 2f, 10f);
    }

}
