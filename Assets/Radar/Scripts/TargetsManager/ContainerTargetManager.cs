using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Контейнер хранящий текущую реализацию интерфейса ITargetManager.  
    /// Можно было бы использовать Zenject для этого дела.
    /// </summary>
    public class ContainerTargetManager : MonoBehaviour
    {
        public ITargetManager TargetManagerContainer;

        private void Awake()
        {
            TargetManagerContainer = gameObject.AddComponent<TargetManager>();
        }
    }
}
