using System;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Класс который будет докладывать когда можно обновлять любые средства радара. 
    /// </summary>
    public class PlayerLocator : MonoBehaviour
    {
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
        /// Обновить локатор принудительно извне (В случа если объект (target) двигается сам.
        /// </summary>
        public void UpdateLocator()
        {
            onUpdateLocator();
        }
    }
}