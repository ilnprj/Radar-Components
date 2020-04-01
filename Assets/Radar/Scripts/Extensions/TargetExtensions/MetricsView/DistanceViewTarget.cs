using UnityEngine.UI;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Distance view metric in uni-meters
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class DistanceViewTarget : AbstractExtensionTarget
    {
        private Text textDistance;
        
        [Header("Image target for alpha check:")]
        [SerializeField]
        private Image imageTarget;

        [Header("Changing alpha text:")]
        [SerializeField]
        private bool changeAlphaText;

        private void Awake()
        {
            textDistance = GetComponent<Text>();
        }

        public override void UpdateExtensionView(Transform playerPosition,ITarget inputTarget)
        {
            Vector3 dif = playerPosition.position - inputTarget.TransformTarget.position;
            float lenght = dif.magnitude;
            textDistance.text = lenght.ToString("0")+"m";

            if (changeAlphaText && imageTarget)
            {
                textDistance.color = new Color(textDistance.color.r, textDistance.color.g, textDistance.color.b, imageTarget.color.a);
            }
        }
    }
}

