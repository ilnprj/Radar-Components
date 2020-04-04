// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using System;
using UnityEngine;

namespace RadarComponents
{

    [RequireComponent(typeof(AudioSource))]
    public class SoundPitchDistance : AbstractExtensionTarget
    {
        [SerializeField]
        private bool soundEnabled = true;

        [SerializeField]
        private float maxDistance = 100.5f;
        [SerializeField]
        private float minDistance = 1.5f;
        [SerializeField]
        private float farPitch = 0.7f;
        [SerializeField]
        private float closePitch = 1.5f;

        public bool SoundEnabled
        {
            get
            {
                return soundEnabled;
            }
            set
            {
                if (soundEnabled != value)
                {
                    soundEnabled = value;
                    onChangeSound();
                }
            }
        }

        private AudioSource source;
        private Action onChangeSound = delegate { };

        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.playOnAwake = true;
            source.loop = true;
            source.Play();
        }

        private void OnEnable()
        {
            ChangeSoundState();
            onChangeSound += ChangeSoundState;
        }

        private void OnDisable()
        {
            onChangeSound -= ChangeSoundState;
        }

        private void ChangeSoundState()
        {
            source.mute = !soundEnabled;
        }

        public override void UpdateExtensionView(Transform player, ITarget inputTarget)
        {
            Vector3 dif = player.position - inputTarget.TransformTarget.position;
            float lenght = dif.magnitude;
            float x = Mathf.Clamp(lenght, minDistance, maxDistance);
            float pitch = (farPitch - closePitch) * (x - minDistance) / (maxDistance - minDistance) + closePitch;
            source.pitch = pitch;
        }
    }
}
