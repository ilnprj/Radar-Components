using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(RawImage))]
public class HorizontalCompass : AbstractRadar
{
    private RawImage compassImage;

    protected override void Awake()
    {
        compassImage = GetComponent<RawImage>();
        base.Awake();
    }

    public override void OnUpdateRadar()
    {
        if (locator != null)
        {
            compassImage.uvRect = new Rect(locator.transform.localEulerAngles.y / 360f, 0f, 1f, 1f);
        }
    }
}
