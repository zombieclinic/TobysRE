using UnityEngine;
using UnityEngine.UIElements;

public class keyCardsUi : MonoBehaviour
{

    [Header("UI")]
      [SerializeField] private UIDocument uiDocument;

    [Header("Player")]
    [SerializeField] private PlayerController player;

    [Header("keyCards")]
    private Image YellowPic;
    private Image RedPic;
    private Image BluePic;

     
    void Start()
    {
        var root = uiDocument.rootVisualElement;

        YellowPic = root.Q<Image>("yellowKeyLabel");
        RedPic = root.Q<Image>("redKeyLabel");
        BluePic = root.Q<Image>("blueKeyLabel");

        SetVisible(YellowPic, false);
        SetVisible(RedPic, false);
        SetVisible(BluePic, false);
    }

       void Update()
    {
        if (player == null) return;

        SetVisible(YellowPic, player.yellowKeyCard);
        SetVisible(RedPic, player.redKeyCard);
        SetVisible(BluePic, player.blueKeyCard);
    }

    private void SetVisible(VisualElement el, bool visible)
    {
        if (el == null) return;
        el.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
    }

}
