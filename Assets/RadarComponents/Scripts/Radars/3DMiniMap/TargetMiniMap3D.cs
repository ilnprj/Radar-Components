// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using UnityEngine;

namespace RadarComponents
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TargetMiniMap3D : BaseTargetView
    {
        private float radarWidth;
        private float radarHeight;

        /// <summary>
        /// Given that the target has the same sides
        /// </summary>
        private float targetWidth;

        private MiniMap3D radarContainer;
        private MeshRenderer modelTargetView;

        protected override void Awake()
        {
            base.Awake();
            radarContainer = GetComponentInParent<MiniMap3D>();
            modelTargetView = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            radarWidth = radarContainer.transform.localScale.x;
            radarHeight = radarContainer.transform.localScale.y;
            transform.SetParent(radarContainer.transform);
            targetWidth = radarWidth * 5 / 100;
            UpdateViewTarget();
        }

        public override void UpdateViewTarget()
        {
            Vector3 playerPos = playerTransform.position;
            Vector3 targetPos = CurrentTarget.TransformTarget.position;
            Vector3 normalisedTargetPosiiton = NormalisedPosition(playerPos, targetPos);
            Vector2 targetPosition = CalculateBlipPosition(normalisedTargetPosiiton);
            targetPosition.x = CheckBorder(targetPosition.x, 5);
            targetPosition.y = CheckBorder(targetPosition.y, 5);

            if (radarContainer.TargetsFadeOut)
            {
                Vector3 dif = targetPos - playerPos;
                float distance = dif.magnitude;
                modelTargetView.enabled = distance <= radarContainer.InsideRadarDistance;
            }

            UpdateResultPosition(targetPosition);
        }

        /// <summary>
        /// Controls the location of the target within 
        /// </summary>
        private float CheckBorder(float position, float border)
        {
            if (position + targetWidth >= border)
            {
                position = border - targetWidth;
            }

            if (position < -5)
            {
                position = -5;
            }
            return position;
        }

        private void UpdateResultPosition(Vector2 position)
        {
            transform.localPosition = new Vector3(position.x, 2, -position.y);
        }

        private Vector3 NormalisedPosition(Vector3 playerPos, Vector3 targetPos)
        {
            float normalisedyTargetX = (targetPos.x - playerPos.x) / radarContainer.transform.localScale.x;
            float normalisedyTargetZ = (targetPos.z - playerPos.z) / radarContainer.transform.localScale.x;
            return new Vector3(normalisedyTargetX, 0, normalisedyTargetZ);
        }

        private Vector2 CalculateBlipPosition(Vector3 targetPos)
        {
            float angleToTarget = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;
            float anglePlayer = playerTransform.eulerAngles.y;

            // Angle
            float angleRadarDegrees = angleToTarget - anglePlayer - 90;

            // Calculate
            float normalisedDistanceToTarget = targetPos.magnitude;
            float angleRadians = angleRadarDegrees * Mathf.Deg2Rad;
            float tarX = normalisedDistanceToTarget * Mathf.Cos(angleRadians);
            float tarY = normalisedDistanceToTarget * Mathf.Sin(angleRadians);

            // Scale
            tarX *= radarWidth / 2;
            tarY *= radarHeight / 2;

            // Offset
            if (radarContainer.OffsetPosition != 0)
            {
                tarX += radarWidth / radarContainer.OffsetPosition;
                tarY += radarHeight / radarContainer.OffsetPosition;
            }
            return new Vector2(tarX, tarY);
        }
    }
}