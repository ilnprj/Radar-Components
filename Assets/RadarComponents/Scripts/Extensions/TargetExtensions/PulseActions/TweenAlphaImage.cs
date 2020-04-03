using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace RadarComponents
{
    [RequireComponent(typeof(Image))]
    public class TweenAlphaImage : AbstractExtensionTarget
    {
        private Image image;
        [Header("Lenght of animation:")]
        [SerializeField]
        private float durationAnim = 0.5f;
        [SerializeField]
        private Color32 startColor = default;
        [SerializeField]
        private Color32 resultColor = default;
        private Coroutine coroutine = null;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
        public override void UpdateExtensionView(Transform player, ITarget inputTarget)
        {
            if (coroutine != null)
            {
                StopCoroutine(LerpImage());
            }
            coroutine = StartCoroutine(LerpImage());
        }

        private IEnumerator LerpImage()
        {
            float ElapsedTime = 0f;
            float end = Time.unscaledDeltaTime + durationAnim;
            while (ElapsedTime < end)
            {
                ElapsedTime += Time.deltaTime;
                image.color = Color.Lerp(startColor, resultColor, (ElapsedTime / end));
                yield return null;
            }
        }
    }
}