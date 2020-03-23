using UnityEngine;
using UnityEngine.UI;

namespace RadarComponents
{
    /// <summary>
    /// Базовый класс цели
    /// </summary>
    public abstract class BaseTargetView : MonoBehaviour
    {
        [SerializeField]
        protected Image iconTarget;
        [SerializeField]
        protected Transform targetTransform;
        [SerializeField]
        protected Transform playerTransform;

        [SerializeField]
        protected RectTransform rootTransform;

        private ITarget target;

        public ITarget Target
        {
            get => target;
            private set
            {
                target = value;
            }
        }

        /// <summary>
        /// Инициализация View
        /// </summary>
        /// <param name="target"></param>
        /// <param name="inputPlayer"></param>
        public void InitTargetView(ITarget target, Transform inputPlayer, RectTransform inputRootTransform)
        {
            rootTransform = inputRootTransform;
            iconTarget.sprite = target.SpriteTarget;
            targetTransform = target.TransformTarget;
            playerTransform = inputPlayer;
            Target = target;
        }

        protected virtual void OnEnable()
        {
            PlayerLocator.onUpdateLocator += UpdateViewTarget;
        }

        protected virtual void OnDisable()
        {
            PlayerLocator.onUpdateLocator -= UpdateViewTarget;
        }

        /// <summary>
        /// Что абстрактный класс будет выполнять при обновлении данных о цели или местоположении игрока
        /// </summary>
        public abstract void UpdateViewTarget();
    }
}