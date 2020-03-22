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

        public List<ITarget> Targets => targets;


        public event Action<ITarget> onAddTarget = delegate { };
        public event Action<ITarget> onRemoveTarget = delegate { };

        public void AddTarget(ITarget target)
        {
            targets.Add(target);
            onAddTarget(target);
            countTargets++;
        }

        public void RemoveTarget(ITarget target)
        {
            targets.Remove(target);
            onRemoveTarget(target);
            countTargets--;
        }
    }
}