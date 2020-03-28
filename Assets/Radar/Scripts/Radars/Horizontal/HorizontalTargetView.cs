using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Item view for Horizontal target on compass
    /// </summary>
    public class HorizontalTargetView : BaseTargetView
    {
        [SerializeField]
        private Color arrowIn = Color.white;
        [SerializeField]
        private Color arrowOut = Color.clear;

        private float minSize;
        private float maxSize;

        private void Start()
        {
            iconTarget.rectTransform.anchoredPosition = new Vector2(0, 0);
            maxSize = iconTarget.rectTransform.sizeDelta.x;
            minSize = maxSize / 2;
            UpdateViewTarget();
        }

        /// <summary>
        /// Метод отрабатывающий в случае если цель или игрок - изменили свое положение
        /// </summary>
        public override void UpdateViewTarget()
        {
            float posX = Camera.main.WorldToScreenPoint(targetTransform.position).x;
            float center = Screen.width / 2;

            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            Vector3 toOther = targetTransform.position - Camera.main.transform.position;
            if (Vector3.Dot(forward, toOther) < 0) posX = 0; 

            float minPos = center - rootTransform.sizeDelta.x / 2;
            float maxPos = center + rootTransform.sizeDelta.x / 2;
            posX = Mathf.Clamp(posX, minPos, maxPos); 

            posX = center - posX; 
            iconTarget.rectTransform.anchoredPosition = new Vector2(-posX, 0); 

            // Color
            Color tmp = Color.Lerp(arrowIn, arrowOut, Mathf.Abs(posX) / (rootTransform.sizeDelta.x / 2));
            iconTarget.color = tmp;

            // Distance
            float dis = Vector3.Distance(playerTransform.position, targetTransform.position);
            float size = maxSize - dis / 4;
            size = Mathf.Clamp(size, minSize, maxSize);
            //FIXME: Идет обнуление по дефолту
            //iconTarget.rectTransform.sizeDelta = new Vector2(size, iconTarget.rectTransform.sizeDelta.y);
        }
    }
}