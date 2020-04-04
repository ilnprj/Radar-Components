// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

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

        [SerializeField]
        private Vector2 offsetVector = default;

        public Vector2 OffsetVector
        {
            get
            {
                return offsetVector;
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


        [Header("Circle Radar Option:")]
        [SerializeField]
        [Space(10)]
        private bool circleRadar = default;

        public bool CircleRadar
        {
            get
            {
                return circleRadar;
            }
        }

        [SerializeField]
        private float circleCenter = default;

        public float CircleCenter
        {
            get
            {
                return circleCenter;
            }
        }

        [SerializeField]
        private Vector2 circlePosition = default;

        public Vector2 CirclePosition
        {
            get
            {
                return circlePosition;
            }
        }

        public override void OnUpdateRadar()
        {
            //NOTE: All functional work in items
        }
    }
}