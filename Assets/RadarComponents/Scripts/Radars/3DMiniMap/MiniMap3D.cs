using UnityEngine;

namespace RadarComponents
{
    public class MiniMap3D : AbstractRadar
    {
        [Range(1f, 1000f)]
        [SerializeField]
        private float insideRadarDistance = 5f;

        [Range(0f,20f)]
        [SerializeField]
        private float offsetPosition = 2f;

        public float OffsetPosition
        {
            get
            {
                return offsetPosition;
            }
        }

        public float InsideRadarDistance
        {
            get
            {
                return insideRadarDistance;
            }
        }

        public override void OnUpdateRadar()
        {
            //Note: All functional works in target items
        }
    }
}