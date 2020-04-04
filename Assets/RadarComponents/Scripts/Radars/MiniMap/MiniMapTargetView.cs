// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using UnityEngine;
using UnityEngine.UI;

namespace RadarComponents
{
    /// <summary>
    /// Script that display target on the radar
    /// </summary>
    public class MiniMapTargetView : BaseTargetView
    {
        private RectTransform rectPositionView;
        private Image imageTargetView;
        private Image _background;
        private Image backgroundRadar
        {
            get
            {
                if (_background == null)
                {
                    _background = rootTransform.GetComponent<Image>();
                    return _background;
                }
                return _background;
            }
        }
        
        private float radarWidth;
        private float radarHeight;
        private float targetHeight;
        private float targetWidth;

        private MiniMapRadar radarContainer;

        protected override void Awake()
        {
            base.Awake();
            rectPositionView = GetComponent<RectTransform>();
            imageTargetView = GetComponent<Image>();
            imageTargetView.preserveAspect = true;
            radarContainer = GetComponentInParent<MiniMapRadar>();
        }

        private void Start()
        {
            radarWidth = backgroundRadar.rectTransform.rect.width;
            radarHeight = backgroundRadar.rectTransform.rect.height;
            targetHeight = radarHeight * radarContainer.TargetViewSize / 100;
            targetWidth = radarWidth * radarContainer.TargetViewSize / 100;
            UpdateViewTarget();
        }

        public override void UpdateViewTarget()
        {
            Vector3 playerPos = playerTransform.position;
            Vector3 targetPos = CurrentTarget.TransformTarget.position;
            Vector3 normalisedTargetPosiiton = NormalisedPosition(playerPos, targetPos);
            Vector2 targetPosition = CalculateBlipPosition(normalisedTargetPosiiton);

            if (radarContainer.TargetsFadeOut)
            {
                Vector3 dif = targetPos - playerPos;
                float distance = dif.magnitude;
                imageTargetView.enabled = distance <= radarContainer.RadarViewDistance;
            }

            if (!radarContainer.CircleRadar)
            {
                targetPosition.x = CheckBorder(targetPosition.x, backgroundRadar.rectTransform.rect.width);
                targetPosition.y = CheckBorder(targetPosition.y, backgroundRadar.rectTransform.rect.height);
            }
            else
            {
                ClampPositionToCircle(new Vector2(60f,60f), 60f, ref targetPosition);
            }
            
            UpdateResultPosition(targetPosition);
        }

        /// <summary>
        /// Method using only for Circle Radar
        /// </summary>
        public void ClampPositionToCircle(Vector2 center, float radius, ref Vector2 position)
        {
            // Calculate the offset vector from the center of the circle to our position
            Vector2 offset = position - center;
            // Calculate the linear distance of this offset vector
            float distance = offset.magnitude;
            if (radius < distance)
            {
                // If the distance is more than our radius we need to clamp
                // Calculate the direction to our position
                Vector2 direction = offset / distance;
                // Calculate our new position using the direction to our old position and our radius
                position = center + direction * radius;
            }
        }

        /// <summary>
        /// Controls the location of the target within. Use only for Square radar
        /// </summary>
        private float CheckBorder(float position, float border)
        {
            if (position + targetWidth > border)
            {
                position = border - targetWidth;
            }

            if (position < 0)
            {
                position = 0;
            }
            return position;
        }

        /// <summary>
        /// Final set position target in radar
        /// </summary>
        /// <param name="position"></param>
        private void UpdateResultPosition(Vector2 position)
        {
            rectPositionView.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, position.x, targetWidth);
            rectPositionView.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, position.y, targetHeight);
            UpdateExtensions();
        }

        private Vector3 NormalisedPosition(Vector3 playerPos, Vector3 targetPos)
        {
            float normalisedyTargetX = (targetPos.x - playerPos.x) / radarContainer.RadarViewDistance;
            float normalisedyTargetZ = (targetPos.z - playerPos.z) / radarContainer.RadarViewDistance;
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
            tarX += radarWidth / radarContainer.OffsetVector.x;
            tarY += radarHeight / radarContainer.OffsetVector.y;

            return new Vector2(tarX, tarY);
        }
    }
}