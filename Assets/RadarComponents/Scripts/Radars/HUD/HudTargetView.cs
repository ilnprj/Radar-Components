using UnityEngine;

namespace RadarComponents {
    /// <summary>
    /// Hud Item View
    /// </summary>
    public class HudTargetView : BaseTargetView
    {
        private float centerX;
        private float centerY;
        private Vector2 posTarget;
        private Vector3 forward;
        private Vector3 toOther;

        private const int DIVIDE_CONSTANT = 2;
        private HudRadar radarContainer;

        protected override void Awake()
        {
            base.Awake();
            radarContainer = GetComponentInParent<HudRadar>();    
        }

        public override void UpdateViewTarget()
        {
            posTarget = new Vector2(locator.CameraPlayer.WorldToScreenPoint(targetTransform.position).x, locator.CameraPlayer.WorldToScreenPoint(targetTransform.position).y);
            centerX = Screen.width / DIVIDE_CONSTANT;
            centerY = Screen.height / DIVIDE_CONSTANT;

            forward  = locator.CameraPlayer.transform.TransformDirection(Vector3.forward);
            toOther = targetTransform.position - Camera.main.transform.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                float yPos = radarContainer.TargetsFadeOut ? -100 : 0;
                posTarget = new Vector2(posTarget.x, yPos);
            }

            if (!radarContainer.TargetsFadeOut)
            {
                CalibrateInsindeScreen();
            }
            
            posTarget = new Vector2(centerX - posTarget.x, centerY - posTarget.y);
            iconTarget.rectTransform.anchoredPosition = new Vector2(-posTarget.x, -posTarget.y);
        }

        private void CalibrateInsindeScreen()
        { 
            float minPos = centerX - rootTransform.sizeDelta.x / DIVIDE_CONSTANT;
            float maxPos = centerX + rootTransform.sizeDelta.x / DIVIDE_CONSTANT;

            float minPosY = centerY - rootTransform.sizeDelta.y / DIVIDE_CONSTANT;
            float maxPosY = centerY + rootTransform.sizeDelta.y / DIVIDE_CONSTANT;

            posTarget = new Vector2(Mathf.Clamp(posTarget.x, minPos + (iconTarget.rectTransform.rect.width / DIVIDE_CONSTANT), maxPos - (iconTarget.rectTransform.rect.width / DIVIDE_CONSTANT)),
                                Mathf.Clamp(posTarget.y, minPosY + (iconTarget.rectTransform.rect.height / DIVIDE_CONSTANT), maxPosY - (iconTarget.rectTransform.rect.height / DIVIDE_CONSTANT)));
        }
    }
}