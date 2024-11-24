using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Door1 : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    Transform selectedDoor;
    GameObject dragPointGameobject;
    int leftDoor = 0;
    [SerializeField]
    LayerMask DoorLayer;

    private AudioSource doorSound;

    public MeshRenderer doorRenderer;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 20, DoorLayer))
        {
            if (Input.GetMouseButtonDown(1))
            {
                //doorSound.Play();
                selectedDoor = hit.collider.gameObject.transform.parent;
                doorSound = selectedDoor.GetComponent<AudioSource>();
                doorSound.Play();
                Debug.Log("SelectedDoor set:" + selectedDoor);
            }
        }

        if (selectedDoor != null)
        {
            HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
            JointMotor motor = joint.motor;

            if (dragPointGameobject == null)
            {
                dragPointGameobject = new GameObject("Ray door");
                dragPointGameobject.transform.parent = selectedDoor;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            dragPointGameobject.transform.position = ray.GetPoint(Vector3.Distance(selectedDoor.position, transform.position));
            dragPointGameobject.transform.rotation = selectedDoor.rotation;

            float delta = Mathf.Pow(Vector3.Distance(dragPointGameobject.transform.position, selectedDoor.position), 3);

            //checking left vs right
            if (doorRenderer.localBounds.center.x > selectedDoor.localPosition.x)
            {
                leftDoor = 1;
            }
            else
            {
                leftDoor = -1;
            }

            float speedMultiplier = 60000;
            if (Mathf.Abs(selectedDoor.parent.forward.z) > 0.5f)
            {
                if (dragPointGameobject.transform.position.x > selectedDoor.position.x)
                {
                    motor.targetVelocity = delta * -speedMultiplier * Time.deltaTime * leftDoor;
                }
                else
                {
                    motor.targetVelocity = delta * speedMultiplier * Time.deltaTime * leftDoor;
                }
            }
            else
            {
                if (dragPointGameobject.transform.position.z > selectedDoor.position.z)
                {
                    motor.targetVelocity = delta * -speedMultiplier * Time.deltaTime * leftDoor;
                }
                else
                {
                    motor.targetVelocity = delta * speedMultiplier * Time.deltaTime * leftDoor;
                }
            }
            joint.motor = motor;

            if (Input.GetMouseButtonUp(1))
            {
                selectedDoor = null;
                motor.targetVelocity = 0;
                joint.motor = motor;
                Destroy(dragPointGameobject);
            }
        }
    }
}
