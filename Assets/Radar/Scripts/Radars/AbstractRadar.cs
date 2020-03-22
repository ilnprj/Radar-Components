using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Абстрактный класс радара. Работает при обновлении локатора игрока
    /// </summary>
    [RequireComponent(typeof(ContainerTargetsView))]
    public abstract class AbstractRadar : MonoBehaviour
    {
        protected PlayerLocator locator = null;
        protected ITargetManager targetManager = null;
        protected ContainerTargetsView containerViews;

        protected virtual void Awake()
        {
            CheckLocator();
            containerViews = GetComponent<ContainerTargetsView>();
        }

        protected virtual void Start()
        {
            targetManager = ContainerTargetManager.targetManager;
            containerViews.SetTargetManager(targetManager);
        }

        private void CheckLocator()
        {
            if (PlayerLocator.IsInited)
            {
                locator = FindObjectOfType<PlayerLocator>();
            }
            else
            {
                PlayerLocator.onInit += SetLocator;
            }
            PlayerLocator.onUpdateLocator += OnUpdateRadar;
        }

        protected virtual void OnDestroy()
        {
            PlayerLocator.onInit -= SetLocator;
            PlayerLocator.onUpdateLocator -= OnUpdateRadar;
        }

        private void SetLocator()
        {
            locator = FindObjectOfType<PlayerLocator>();
        }

        /// <summary>
        /// Обновить данные радара в его реализации
        /// </summary>
        public abstract void OnUpdateRadar();
    }
}