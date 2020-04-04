// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Abstract radar class. Works when updating a player’s locator
    /// </summary>
    [RequireComponent(typeof(ContainerTargetsView))]
    public abstract class AbstractRadar : MonoBehaviour
    {
        public bool TargetsFadeOut
        {
            get
            {
                return targetsFadeOut;
            }
        }

        protected PlayerLocator locator = null;
        protected ContainerTargetsView containerViews;

        [SerializeField]
        private bool targetsFadeOut = default;
        private ContainerTargetManager containerTargets;

        protected virtual void Awake()
        {
            CheckLocator();
            containerViews = GetComponent<ContainerTargetsView>();
            containerTargets = FindObjectOfType<ContainerTargetManager>();
        }

        protected virtual void Start()
        {
            containerViews.SetTargetManager(containerTargets.TargetManager);
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
        /// Update information on radar
        /// </summary>
        public abstract void OnUpdateRadar();
    }
}