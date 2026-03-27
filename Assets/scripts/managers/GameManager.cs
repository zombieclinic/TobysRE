using UnityEngine;

public class GameManager : MonoBehaviour


{
    public static GameManager Instance;

    [Header("Keycard Spawn Indexes")]
    public int redKeycardIndex = -1;
    public int yellowKeycardIndex = -1;
    public int blueKeycardIndex = -1;

    [Header("Trapdoor States")]
    public bool redTrapdoorOpen = false;
    public bool blueTrapdoorOpen = false;
    public bool yellowTrapdoorOpen = false;

    [Header("Keycard Collected")]
    public bool redKeycardPickedUp = false;
    public bool yellowKeycardPickedUp = false;
    public bool blueKeycardPickedUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
