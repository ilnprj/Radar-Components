using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// Base functional for Target View
    /// </summary>
    public abstract class BaseTargetView : MonoBehaviour
    {
        [SerializeField]
        protected Image iconTarget;
        [SerializeField]
        protected List<AbstractExtensionTarget> extensionsForView = new List<AbstractExtensionTarget>();

        protected Transform targetTransform;
        protected Transform playerTransform;
        protected RectTransform rootTransform;
        protected PlayerLocator locator;

        public ITarget CurrentTarget { get; private set; }

        /// <summary>
        /// Init View
        /// </summary>
        public void InitTargetView(ITarget target, Transform inputPlayer, RectTransform inputRootTransform)
        {
            rootTransform = inputRootTransform;
            if (iconTarget != null)
            {
                iconTarget.sprite = target.SpriteTarget;
            }
            targetTransform = target.TransformTarget;
            playerTransform = inputPlayer;
            CurrentTarget = target;
        }

        protected virtual void OnEnable()
        {
            PlayerLocator.onUpdateLocator += UpdateViewTarget;
        }

        protected virtual void Awake()
        {
            locator = FindObjectOfType<PlayerLocator>();
        }

        protected virtual void OnDisable()
        {
            PlayerLocator.onUpdateLocator -= UpdateViewTarget;
        }

        /// <summary>
        /// What should an abstract class do when updating data about a player’s goal or location
        /// </summary>
        public abstract void UpdateViewTarget();

        protected void UpdateExtensions()
        {
            foreach (var item in extensionsForView)
            {
                item.UpdateExtensionView(playerTransform, CurrentTarget);
            }
        }
    }
}