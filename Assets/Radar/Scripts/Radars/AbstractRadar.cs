using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Abstract radar class. Works when updating a player’s locator
    /// </summary>
    [RequireComponent(typeof(ContainerTargetsView))]
    public abstract class AbstractRadar : MonoBehaviour
    {
        protected PlayerLocator locator = null;
        protected ITargetManager targetManager = null;
        protected ContainerTargetsView containerViews;

        private ContainerTargetManager containerTargets;

        protected virtual void Awake()
        {
            CheckLocator();
            containerViews = GetComponent<ContainerTargetsView>();
            containerTargets = FindObjectOfType<ContainerTargetManager>();
        }

        protected virtual void Start()
        {
            targetManager = containerTargets.TargetManager;
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