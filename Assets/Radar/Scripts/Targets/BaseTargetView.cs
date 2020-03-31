using UnityEngine;
using UnityEngine.UI;

namespace RadarComponents
{
    /// <summary>
    /// Base functional for Target View
    /// </summary>
    public abstract class BaseTargetView : MonoBehaviour
    {
        [SerializeField]
        protected Image iconTarget;
        protected Transform targetTransform;
        protected Transform playerTransform;
        protected RectTransform rootTransform;

        public ITarget Target { get; private set; }

        /// <summary>
        /// Init View
        /// </summary>
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
        /// What should an abstract class do when updating data about a player’s goal or location
        /// </summary>
        public abstract void UpdateViewTarget();
    }
}