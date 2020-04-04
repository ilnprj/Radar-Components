using UnityEngine;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// Class of the target is registered when the target appears on the scene in the manager, and removes it from there upon deactivation.
    /// The target can optionally update the player’s locator itself, if it has the ability to move
    /// </summary>
    public class Target : MonoBehaviour, ITarget
    {
        [SerializeField]
        private bool isTargetCanMove = default;

        [SerializeField]
        private Sprite defaultIcon = default;

        [SerializeField]
        private List<TypeIconTarget> targetIcons = new List<TypeIconTarget>();

        public List<TypeIconTarget> IconsTarget => targetIcons;

        [SerializeField]
        private string idTarget = default;

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
        public Sprite DefaultIcon => defaultIcon;

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
        /// Actions when activating a target on a scene
        /// </summary>
        public void OnTargetEnable()
        {
            container.TargetManager.AddTarget(this);
        }

        /// <summary>
        /// Actions when the target is deactivated on the scene(The method can also be called if the target can be active but not used)
        /// </summary>
        public void OnTargetDisable()
        {
            container.onInit -= OnTargetEnable;
            container.TargetManager.RemoveTarget(this);
        }
    }
}