using UnityEngine;
using UnityEngine.UI;

namespace RadarComponents
{
    /// <summary>
    /// Script that display target on the radar
    /// </summary>
    public class MiniMapTargetView : BaseTargetView
    {
        [Header("Show target in border:")]
        [SerializeField]
        private bool showInBorder = default;

        [SerializeField]
        private float insideRadarDistance = 20f;

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
            radarContainer = FindObjectOfType<MiniMapRadar>();
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
            targetPosition.x = CheckBorder(targetPosition.x, backgroundRadar.rectTransform.rect.width);
            targetPosition.y = CheckBorder(targetPosition.y, backgroundRadar.rectTransform.rect.height);

            if (!showInBorder)
            {
                Vector3 dif = targetPos - playerPos;
                float distance = dif.magnitude;
                imageTargetView.enabled = distance <= insideRadarDistance;
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

            if (position < 0)
            {
                position = 0;
            }
            return position;
        }

        private void UpdateResultPosition(Vector2 position)
        {
            rectPositionView.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, position.x, targetWidth);
            rectPositionView.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, position.y, targetHeight);
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
            tarX += radarWidth / 2;
            tarY += radarHeight / 2;

            return new Vector2(tarX, tarY);
        }
    }
}