using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Abstract class for target display extensions.
    /// </summary>
    public abstract class AbstractExtensionTarget : MonoBehaviour
    {
        /// <summary>
        /// This method must be called in the BaseTargetView implementation
        /// </summary>
        public abstract void UpdateExtensionView(Transform player, ITarget inputTarget);
    }
}
