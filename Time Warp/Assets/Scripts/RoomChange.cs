using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public GameObject A;
    public GameObject B;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        { 
            if (A.activeInHierarchy)
            {
                A.SetActive(false);
                B.SetActive(true);
            }
            else if (B.activeInHierarchy)
            {
                B.SetActive(false);
                A.SetActive(true);
            }
        }
    }
}
