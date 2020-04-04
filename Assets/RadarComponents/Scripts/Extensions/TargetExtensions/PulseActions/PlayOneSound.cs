// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Play one sound. This component must be thrown into the PulseDistance script.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PlayOneSound : AbstractExtensionTarget
    {
        private AudioSource source;
        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.loop = false;
        }

        public override void UpdateExtensionView(Transform player, ITarget inputTarget)
        {
            source.PlayOneShot(source.clip, 1.0f);
        }
    }
}
