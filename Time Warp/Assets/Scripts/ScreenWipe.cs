using UnityEngine;
using UnityEngine.UI;

public class ScreenWipe : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float wipeSpeed = 1f;

    private Image image;

    public enum WipeMode { NotBlocked, WipingToNotBlocked, Blocked, WipingToBlocked }

    private WipeMode wipeMode = WipeMode.NotBlocked;

    private float wipeProgress;

    //public static bool isDone = false;
    public bool isDone { get; private set; }

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleWipe(bool blockScreen)
    {
        isDone = false;  // Ensure isDone is reset at the start of each wipe
        wipeMode = blockScreen ? WipeMode.WipingToBlocked : WipeMode.WipingToNotBlocked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (wipeMode)
        {
            case WipeMode.WipingToBlocked:
                WipeToBlocked();
                break;
            case WipeMode.WipingToNotBlocked:
                WipeToNotBlocked();
                break;
            case WipeMode.Blocked:
            case WipeMode.NotBlocked:
                // Do nothing, transition is complete
                break;
        }
    }

    private void WipeToBlocked()
    {
        wipeProgress += Time.deltaTime * (1f / wipeSpeed);
        image.fillAmount = wipeProgress;
        Debug.Log(wipeProgress + "WIPE PROGRESS");
        if (wipeProgress >= 1f)
        {
            isDone = true;
            wipeMode = WipeMode.Blocked;
            Debug.Log("WipeToBlocked done.");
        }
    }

    private void WipeToNotBlocked()
    {
        wipeProgress -= Time.deltaTime * (1f / wipeSpeed);
        image.fillAmount = wipeProgress;
        if (wipeProgress <= 0)
        {
            isDone = true;
            wipeMode = WipeMode.NotBlocked;
            Debug.Log("WipeToNOTBlocked done.");
        }
    }

    [ContextMenu("Block")]
    private void Block() { ToggleWipe(true); }
    [ContextMenu("Clear")]
    private void Clear() { ToggleWipe(false); }
}
