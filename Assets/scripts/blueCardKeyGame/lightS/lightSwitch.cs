using UnityEngine;

public class lightSwitch : MonoBehaviour,IInteractable
{
    public Sprite on;
    public Sprite off;
    private SpriteRenderer sr;
    private bool isOn = false;
    public GameObject Light;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
   public void Interact(PlayerBrain player)
    {
       Debug.Log("player.interacted");
       isOn = !isOn;
        sr.sprite = isOn ? on : off;

        if (isOn)
        {
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
        }
    }
}
