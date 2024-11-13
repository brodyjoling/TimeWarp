using UnityEngine;

public class InsertStabalizationDevice : MonoBehaviour
{
    private bool nearCore = false;
    private Vector3 corePosition;
    public Collider coreCollider;

    private GameObject stabalizationDevice;
    //public Rigidbody theone; //change
    public GameObject insertText;
    public Texture insertedTexture;
    public MeshRenderer brokenStabalizationDevice;

    private LevelManager lvlMgr;

    void Start()
    {
        GameObject levelManagerObject = GameObject.Find("LevelManager");
        lvlMgr = levelManagerObject.GetComponent<LevelManager>();
        corePosition = coreCollider.transform.position;
        stabalizationDevice = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseDrag.isDragging && nearCore)
        {
            insertText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                insertText.SetActive(false);
                stabalizationDevice.SetActive(false);
                brokenStabalizationDevice.material.mainTexture = insertedTexture;
                InsertDevice();
            }
        }
        else
        {
            insertText.SetActive(false);
        }
    }

    private void InsertDevice()
    {
        ResetAllGravity();
        Debug.Log("Inserted Stabalization Device");
    }

    private void ResetAllGravity()
    {
        RandomGravity[] objectsWithRandomGravity = Object.FindObjectsByType<RandomGravity>(FindObjectsSortMode.None);
        RandomGravityStart[] objectsWithRandomGravity1 = Object.FindObjectsByType<RandomGravityStart>(FindObjectsSortMode.None);


        foreach (RandomGravity objectWithRandomGravity in objectsWithRandomGravity)
        {
            objectWithRandomGravity.ResetGravity();
        }
        Debug.Log("Gravity Stabalized");

        foreach (RandomGravityStart objectWithRandomGravity1 in objectsWithRandomGravity1)
        {
            objectWithRandomGravity1.ResetGravity();
        }
        Debug.Log("Gravity Stabalized");
        //implement wait
        lvlMgr.startNextLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == coreCollider)
        {
            nearCore = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == coreCollider)
        {
            nearCore = false;
        }
    }
}
