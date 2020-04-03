using System;
using System.Collections.Generic;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Реализация TargetManager в простом виде
    /// </summary>
    public class TargetManager : MonoBehaviour, ITargetManager
    {        
        public List<ITarget> Targets { get; private set; } = new List<ITarget>();
        public event Action<ITarget> onAddTarget = delegate { };
        public event Action<ITarget> onRemoveTarget = delegate { };

        [SerializeField]
        private int countTargets = 0;

        /// <summary>
        /// Добавить цель
        /// </summary>
        /// <param name="target"></param>
        public void AddTarget(ITarget target)
        {
            Targets.Add(target);
            onAddTarget(target);
            //TODO: Удалить после того как фича будет отлажена
            countTargets++;
        }

        /// <summary>
        /// Убрать цель
        /// </summary>
        /// <param name="target"></param>
        public void RemoveTarget(ITarget target)
        {
            Targets.Remove(target);
            onRemoveTarget(target);
            countTargets--;
        }
    }
}