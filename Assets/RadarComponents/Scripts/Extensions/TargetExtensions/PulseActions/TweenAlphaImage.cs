using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace RadarComponents
{
    [RequireComponent(typeof(Image))]
    public class TweenAlphaImage : AbstractExtensionTarget
    {
        [Header("Ping Pong")]
        [SerializeField]
        private bool pingPong = default;
        [Header("Lenght of animation:")]
        [SerializeField]
        private float durationAnim = 0.5f;
        [SerializeField]
        private Color32 startColor = default;
        [SerializeField]
        private Color32 resultColor = default;
        private Coroutine coroutine = null;
        private Image image;
        private bool busy = false;
        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public override void UpdateExtensionView(Transform player, ITarget inputTarget)
        {
            StartLerp();
        }

        private IEnumerator LerpImage()
        {
            float ElapsedTime = 0f;
            float end = Time.unscaledDeltaTime + durationAnim;
            image.enabled = true;
            while (ElapsedTime < end)
            {
                ElapsedTime += Time.deltaTime;
                if (pingPong)
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong((ElapsedTime / end), durationAnim));
                else
                image.color = Color.Lerp(resultColor, startColor, (ElapsedTime / end));
                yield return null;
            }
            image.enabled = !pingPong;
        }

        public void StartLerp()
        {
            if (!busy || !pingPong)
            {
                if (coroutine != null)
                {
                    StopCoroutine(LerpImage());
                }
                coroutine = StartCoroutine(LerpImage());
            }
        }
    }
}