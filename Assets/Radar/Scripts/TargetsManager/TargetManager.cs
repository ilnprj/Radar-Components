using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadarComponents
{
    public class TargetManager : MonoBehaviour, ITargetManager
    {
        private List<ITarget> targets = new List<ITarget>();

        [SerializeField]
        private int countTargets = 0;

        public List<ITarget> Targets {
            get => targets;
            private set {
                targets = value;
            }
        }

        public event Action<ITarget> onAddTarget = delegate { };
        public event Action<ITarget> onRemoveTarget = delegate { };

        /// <summary>
        /// Добавить цель
        /// </summary>
        /// <param name="target"></param>
        public void AddTarget(ITarget target)
        {
            targets.Add(target);
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
            targets.Remove(target);
            onRemoveTarget(target);
            countTargets--;
        }
    }
}