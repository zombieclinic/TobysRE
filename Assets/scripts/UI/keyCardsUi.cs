using UnityEngine;
using UnityEngine.UIElements;

public class keyCardsUi : MonoBehaviour
{

    [Header("UI")]
      [SerializeField] private UIDocument uiDocument;

    [Header("Player")]
    [SerializeField] private PlayerBrain player;

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

       
    }

    private void SetVisible(VisualElement el, bool visible)
    {
        if (el == null) return;
        el.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
    }

}
