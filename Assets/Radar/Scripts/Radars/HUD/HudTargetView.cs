using UnityEngine;

namespace RadarComponents {
    /// <summary>
    /// Hud Item View
    /// </summary>
    public class HudTargetView : BaseTargetView
    {
        public override void UpdateViewTarget()
        {
            float posX = locator.CameraPlayer.WorldToScreenPoint(targetTransform.position).x;
            float posY = locator.CameraPlayer.WorldToScreenPoint(targetTransform.position).y;
            float centerX = Screen.width / 2;
            float centerY = Screen.height / 2;

            Vector3 forward = locator.CameraPlayer.transform.TransformDirection(Vector3.forward);
            Vector3 toOther = targetTransform.position - Camera.main.transform.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                posX = 0;
                posY = 0;
            }

            float minPos = centerX - rootTransform.sizeDelta.x / 2;
            float maxPos = centerX + rootTransform.sizeDelta.x / 2;

            float minPosY = centerY - rootTransform.sizeDelta.y / 2;
            float maxPosY = centerY + rootTransform.sizeDelta.y / 2;
            //FIXME: Слишком паршиво выглядит
            posX = Mathf.Clamp(posX, minPos+(iconTarget.rectTransform.rect.width/2), maxPos-(iconTarget.rectTransform.rect.width/2));
            posY = Mathf.Clamp(posY, minPosY+(iconTarget.rectTransform.rect.height/2), maxPosY-(iconTarget.rectTransform.rect.height/2));
            posX = centerX - posX;
            posY = centerY - posY;
            iconTarget.rectTransform.anchoredPosition = new Vector2(-posX, -posY);
        }
    }
}