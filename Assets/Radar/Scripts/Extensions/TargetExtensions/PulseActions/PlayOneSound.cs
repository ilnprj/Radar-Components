using UnityEngine;

namespace RadarComponents
{
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
