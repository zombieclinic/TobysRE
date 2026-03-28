using UnityEngine;
using UnityEngine.SceneManagement;

public class trapdoor_keycard : MonoBehaviour, IInteractable
{
    public enum TrapdoorColor
    {
        Red,
        Blue,
        Yellow
    }

    [Header("Sprites")]
    [SerializeField] private Sprite Open;
    [SerializeField] private Sprite Closed;

    [Header("Trapdoor Type")]
    [SerializeField] private TrapdoorColor color;

    [Header("Scene To Load")]
    [SerializeField] private string sceneToLoad;

    private SpriteRenderer sr;
    private bool IsOpen = false;

    private GameObject Player;
    private GameObject MainCamera;
    private GameObject CineMachine;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        CineMachine = GameObject.FindGameObjectWithTag("CineMachine");
    }

    void Start()
    {
        LoadTrapdoorState();
    }

    public void LoadTrapdoorState()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null.");
            return;
        }

        switch (color)
        {
            case TrapdoorColor.Red:
                IsOpen = GameManager.Instance.redTrapdoorOpen;
                break;

            case TrapdoorColor.Blue:
                IsOpen = GameManager.Instance.blueTrapdoorOpen;
                break;

            case TrapdoorColor.Yellow:
                IsOpen = GameManager.Instance.yellowTrapdoorOpen;
                break;
        }

        sr.sprite = IsOpen ? Open : Closed;
    }

    public void Interact(PlayerBrain player)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null.");
            return;
        }

        if (!IsOpen)
        {
            IsOpen = true;
            sr.sprite = Open;

SaveTrapdoorState(true);
        }          

        if (Player != null) DontDestroyOnLoad(Player);
        if (MainCamera != null) DontDestroyOnLoad(MainCamera);
        if (CineMachine != null) DontDestroyOnLoad(CineMachine);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("No scene assigned for " + gameObject.name);
        }

        switch (color)
        {
            case TrapdoorColor.Red:
                Debug.LogError("RedCard is Loaded");
                
                break;
            case TrapdoorColor.Blue:
                DontDestroyOnLoad(Player);
                DontDestroyOnLoad(MainCamera);
                DontDestroyOnLoad(CineMachine);
                SceneManager.LoadScene("blueCardKeyGame");
                break;
            case TrapdoorColor.Yellow:
                Debug.LogError("YellowCard is Loaded");
                break;
        }
    }

    private void SaveTrapdoorState(bool open)
    {
    switch (color)
        {
            case TrapdoorColor.Red:
                GameManager.Instance.redTrapdoorOpen = open;
                break;

            case TrapdoorColor.Blue:
                GameManager.Instance.blueTrapdoorOpen = open;
                break;

            case TrapdoorColor.Yellow:
                GameManager.Instance.yellowTrapdoorOpen = open;
                break;
        }

        
    }
}