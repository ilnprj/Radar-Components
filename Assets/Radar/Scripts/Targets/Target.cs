using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Класс цели, регистрирует при появлении на сцене цель в Менеджере и убирает ее оттуда при деактивации
    /// Цель может опционально обновлять локатор игрока сама, если обладает возможностью передвижения
    /// </summary>
    public class Target : MonoBehaviour, ITarget
    {
        [SerializeField]
        private bool isTargetCanMove = default;

        [SerializeField]
        private Sprite targetImage = default;

        [SerializeField]
        private string idTarget = default;
        public Sprite SpriteTarget { get => targetImage; }

        public Transform TransformTarget => transform;

        public string IdTarget => idTarget;

        private ContainerTargetManager container;

        private Vector3 _pos;
        private Vector3 cachedPosition
        {
            set
            {
                if (value!= _pos)
                {
                    _pos = value;
                    PlayerLocator.onUpdateLocator();
                }
            }
        }

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

        private void FixedUpdate()
        {
            if (isTargetCanMove)
            {
                cachedPosition = transform.position;
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