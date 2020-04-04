// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace RadarComponents
{
    [RequireComponent(typeof(Image))]
    public class TweenAlphaImage : AbstractExtensionTarget
    {
        [Header("Ping Radar Option")]
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
            image.enabled = !pingPong;
        }

        public override void UpdateExtensionView(Transform player, ITarget inputTarget)
        {
            StartLerp();
        }

        /// <summary>
        /// StartCoroutine if she already played
        /// </summary>
        public void StartLerp()
        {
            if (!busy || !pingPong)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = pingPong ? StartCoroutine(LerpPing()) : StartCoroutine(LerpOnce());
            }
        }

        //FIXME: This part code is soooooooooooooooooo bad >:)
        private IEnumerator LerpOnce()
        {
            float ElapsedTime = 0f;
            float end = Time.unscaledDeltaTime + durationAnim;
            busy = true;
            while (ElapsedTime < end)
            {
                ElapsedTime += Time.deltaTime;
                image.color = Color.Lerp(startColor, resultColor, (ElapsedTime / end));
                yield return null;
            }
            busy = false;
        }

        private IEnumerator LerpPing()
        {
            float ElapsedTime = 0f;
            float end = Time.unscaledDeltaTime + durationAnim;
            busy = true;
            image.enabled = true;
            while (ElapsedTime < end)
            {
                ElapsedTime += Time.deltaTime;
                image.color = Color.Lerp(startColor, resultColor, (ElapsedTime / end));
                yield return null;
            }
            ElapsedTime = 0f;
            end = Time.unscaledDeltaTime + durationAnim;
            while (ElapsedTime < end)
            {
                ElapsedTime += Time.deltaTime;
                image.color = Color.Lerp(resultColor, startColor, (ElapsedTime / end));
                yield return null;
            }
            busy = false;
            image.enabled = false;
        }
    }
}