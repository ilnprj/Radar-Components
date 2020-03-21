using System.Linq;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Класс цели, регистрирует
    /// </summary>
    public class Target :  MonoBehaviour, ITarget
    {
        //Вот здесь идеальное место для инъекции
        private ITargetManager targetManager;

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            OnTargetEnable();
        }

        private void OnDisable()
        {
            OnTargetDisable();
        }

        public void OnTargetEnable()
        {
            
        }

        public void OnTargetDisable()
        {
           
        }
    }
}
