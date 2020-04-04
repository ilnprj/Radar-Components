// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

namespace RadarComponents
{
    /// <summary>
    /// A script that sends signals at intervals that depend on the distance to the target
    /// </summary>
    public class PulseDistance : AbstractExtensionTarget
    {
        [SerializeField]
        private List<AbstractExtensionTarget> itemsOnPulse = new List<AbstractExtensionTarget>();

        private UnityEvent onPulseUnityEvent = default;

        [SerializeField]
        private float closePulse = 0.25f;

        [SerializeField]
        private float farPulse = 1.0f;

        [SerializeField]
        private float maxDistance = 100f;

        [SerializeField]
        private float minDistance = 1.5f;

        private float currentPulse = 1;
        private Coroutine coroutine;
        private Transform _player;
        private ITarget _target;

        private void OnEnable()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(PulseCoroutine());
        }

        private void OnDisable()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        public override void UpdateExtensionView(Transform player, ITarget inputTarget)
        {
            _player = player;
            _target = inputTarget;
        }

        private IEnumerator PulseCoroutine()
        {
            while (enabled)
            {
                yield return new WaitForSecondsRealtime(currentPulse);

                if (_player && _target != null)
                {
                    Vector3 dif = _player.position - _target.TransformTarget.position;
                    float lenght = dif.magnitude;
                    float x = Mathf.Clamp(lenght, minDistance, maxDistance);
                    currentPulse = (farPulse - closePulse) * (x - minDistance) / (maxDistance - minDistance) + closePulse;
                    foreach (var item in itemsOnPulse)
                    {
                        item.UpdateExtensionView(_player, _target);
                    }
                    if (onPulseUnityEvent != null)
                        onPulseUnityEvent.Invoke();
                }
            }
        }
    }
}