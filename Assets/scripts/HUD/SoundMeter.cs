using UnityEngine;
using UnityEngine.UI;

public class SoundMeterSimple : MonoBehaviour
{
    [SerializeField] private PlayerNoise noise;

    [Header("Bars")]
    [SerializeField] private Image firstLeft;
    [SerializeField] private Image firstRight;

    [SerializeField] private Image secondLeft;
    [SerializeField] private Image secondRight;

    [SerializeField] private Image thirdLeft;
    [SerializeField] private Image thirdRight;

    [SerializeField] private Image fourthLeft;
    [SerializeField] private Image fourthRight;

    [Header("On Colors")]
    [SerializeField] private Color firstColor = new Color(0.1f, 0.85f, 0.1f, 1f);   // green
    [SerializeField] private Color secondColor = new Color(0.8f, 0.8f, 0.1f, 1f);   // yellow
    [SerializeField] private Color thirdColor = new Color(0.95f, 0.5f, 0.1f, 1f);   // orange
    [SerializeField] private Color fourthColor = new Color(0.85f, 0.1f, 0.1f, 1f);  // red

    [Header("Off Color")]
    [SerializeField] private Color offColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    private void Update()
    {
        if (noise == null) return;

        float n = noise.CurrentNoise / noise.MaxNoise;

        SetPair(firstLeft, firstRight, n > 0.01f, firstColor);
        SetPair(secondLeft, secondRight, n > 0.25f, secondColor);
        SetPair(thirdLeft, thirdRight, n > 0.5f, thirdColor);
        SetPair(fourthLeft, fourthRight, n > 0.75f, fourthColor);
    }

    private void SetPair(Image left, Image right, bool on, Color onColor)
    {
        if (left != null)
            left.color = on ? onColor : offColor;

        if (right != null)
            right.color = on ? onColor : offColor;
    }
}