using System;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// A class that will report when it is possible to update any means of the radar.
    /// </summary>
    public class PlayerLocator : MonoBehaviour
    {
        [SerializeField]
        private Camera cameraPlayer = default;

        public Camera CameraPlayer
        {
            get
            {
                return cameraPlayer;
            }
        }

        public static bool IsInited
        {
            get
            {
                return isInited;
            }

            private set
            {
                isInited = value;
                if (value)
                {
                    onInit();
                }
            }
        }

        public static Action onUpdateLocator = delegate { };

        public static event Action onInit = delegate { };

        private static bool isInited = false;

        private Vector3 _position;

        private Quaternion _rotation;
        private Vector3 lastPosition
        {
            set
            {
                if (_position != value)
                {
                    _position = value;
                    onUpdateLocator();
                }
            }
        }

        private Quaternion lastRotation
        {
            set
            {
                if (_rotation != value)
                {
                    _rotation = value;
                    onUpdateLocator();
                }
            }
        }

        private void Awake()
        {
            IsInited = true;
        }

        private void FixedUpdate()
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }

        /// <summary>
        /// Update the locator forcibly from the outside(In case the object (target) moves by itself.
        /// </summary>
        public void UpdateLocator()
        {
            onUpdateLocator();
        }
    }
}