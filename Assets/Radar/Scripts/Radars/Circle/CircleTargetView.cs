using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// View для префаба цели на компасе 
    /// </summary>
    public class CircleTargetView : BaseTargetView
    {
        private GameObject lookingObject;
        private PlayerLocator locator;
        private Transform looking;
        private Transform target;

        private void Awake()
        {
            locator = FindObjectOfType<PlayerLocator>();
            lookingObject = new GameObject();
            lookingObject.transform.SetParent(locator.transform);
            looking = lookingObject.transform;
            looking.localPosition = Vector3.zero;
            looking.localRotation = Quaternion.identity;
        }

        private void Start()
        {
            UpdateViewTarget();
        }

        public override void UpdateViewTarget()
        {
            if (targetTransform!=null)
            {
                looking.LookAt(targetTransform);
                transform.localRotation = Quaternion.Euler(0, 0, -looking.transform.eulerAngles.y);
            }
        }
    }
}

