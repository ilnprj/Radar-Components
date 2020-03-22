using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Класс цели, регистрирует
    /// </summary>
    public class Target :  MonoBehaviour, ITarget
    {
        [SerializeField]
        private Sprite targetImage;

        [SerializeField]
        private string idTarget;
        public Sprite SpriteTarget { get => targetImage; }

        public Transform TransformTarget => transform;

        public string IdTarget => idTarget;

        private void OnEnable()
        {
            OnTargetEnable();
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
            if (ContainerTargetManager.targetManager!=null)
            ContainerTargetManager.targetManager.AddTarget(this);
        }

        /// <summary>
        /// Что делаем при деактивации цели на сцене (Метод можно вызвать и в случае если цель может быть активна но не используема)
        /// </summary>
        public void OnTargetDisable()
        {
            if (ContainerTargetManager.targetManager != null)
                ContainerTargetManager.targetManager.RemoveTarget(this);
        }
    }
}
