using UnityEngine;
using UnityEngine.UIElements;

public class keyCardsUi : MonoBehaviour
{
    private UIDocument uiDocument;
    private PlayerBrain player;

    private Image YellowPic;
    private Image RedPic;
    private Image BluePic;

    private bool uiSetup = false;

    void Update()
    {
        // 🔹 Find UI Document if missing
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();

            if (uiDocument == null)
            {
                uiDocument = FindFirstObjectByType<UIDocument>();
            }

            if (uiDocument != null && !uiSetup)
            {
                SetupUI();
            }
        }

    
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerBrain>();
            }
        }

        if (player == null || !uiSetup) return;

        // 🔹 Update UI
        SetVisible(YellowPic, player.yellowKeyCard);
        SetVisible(RedPic, player.redKeyCard);
        SetVisible(BluePic, player.blueKeyCard);
    }

    private void SetupUI()
    {
        var root = uiDocument.rootVisualElement;

        YellowPic = root.Q<Image>("yellowKeyLabel");
        RedPic = root.Q<Image>("redKeyLabel");
        BluePic = root.Q<Image>("blueKeyLabel");

        SetVisible(YellowPic, false);
        SetVisible(RedPic, false);
        SetVisible(BluePic, false);

        uiSetup = true;
    }

    private void SetVisible(VisualElement el, bool visible)
    {
        if (el == null) return;
        el.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
    }
}