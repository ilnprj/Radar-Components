using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Класс цели, регистрирует
    /// </summary>
    public class Target : MonoBehaviour, ITarget
    {
        [SerializeField]
        private Sprite targetImage;

        [SerializeField]
        private string idTarget;
        public Sprite SpriteTarget { get => targetImage; }

        public Transform TransformTarget => transform;

        public string IdTarget => idTarget;

        private ContainerTargetManager container;

        private void OnEnable()
        {
            container = FindObjectOfType<ContainerTargetManager>();
            if (container.IsInited)
                OnTargetEnable();
            else
            {
                container.onInit += OnTargetEnable;
            }
        }

        private void OnDisable()
        {
            OnTargetDisable();
        }

        /// <summary>
        /// Что делаем при активации цели на сцене
        /// </summary>
        public void OnTargetEnable()
        {
            container.TargetManager.AddTarget(this);
        }

        /// <summary>
        /// Что делаем при деактивации цели на сцене (Метод можно вызвать и в случае если цель может быть активна но не используема)
        /// </summary>
        public void OnTargetDisable()
        {
            container.onInit -= OnTargetEnable;
            container.TargetManager.RemoveTarget(this);
        }
    }
}
