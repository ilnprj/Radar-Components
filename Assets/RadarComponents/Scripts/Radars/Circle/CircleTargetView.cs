using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// View for circle compass
    /// </summary>
    public class CircleTargetView : BaseTargetView
    {
        private GameObject lookingObject;
        private Transform looking;
        private Transform target;

        protected override void Awake()
        {
            base.Awake();
            lookingObject = new GameObject();
            lookingObject.transform.SetParent(locator.transform);
            looking = lookingObject.transform;
            looking.localPosition = Vector3.zero;
            looking.localRotation = Quaternion.identity;
        }

        private void Start()
        {
            lookingObject.name = CurrentTarget.IdTarget;
            UpdateViewTarget();
        }

        public override void UpdateViewTarget()
        {
            if (targetTransform!=null)
            {
                looking.LookAt(targetTransform);
                transform.localRotation = Quaternion.Euler(0, 0, -looking.transform.eulerAngles.y);
                UpdateExtensions();
            }
        }
    }
}

