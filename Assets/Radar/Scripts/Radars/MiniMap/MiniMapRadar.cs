using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Realization of an abstract radar under a mini map
    /// </summary>
    public class MiniMapRadar : AbstractRadar
    {
        [Range(1f, 1000f)]
        [Header("Distance inside radar:")]
        [SerializeField]
        private float distanceInsideView = 10f;

        [Range(1f, 20f)]
        [SerializeField]
        private float offsetPosition = 2f;

        public float OffsetPosition
        {
            get
            {
                return offsetPosition;
            }
        }

        public float RadarViewDistance
        {
            get {
                return distanceInsideView;
            }
        }

        [Header("Size Target View:")]
        [SerializeField]
        private float targetViewSize = 5;

        public float TargetViewSize
        {
            get
            {
                return targetViewSize;
            }
        }

        public override void OnUpdateRadar()
        {
           //NOTE: All functional work in items
        }
    }
}