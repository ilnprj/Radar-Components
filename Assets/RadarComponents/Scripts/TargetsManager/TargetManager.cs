// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Generalization ITargetManager
    /// </summary>
    public class TargetManager : MonoBehaviour, ITargetManager
    {        
        public List<ITarget> Targets { get; private set; } = new List<ITarget>();
        public event Action<ITarget> onAddTarget = delegate { };
        public event Action<ITarget> onRemoveTarget = delegate { };
        public int CountTargets { get; private set; } = 0;

        /// <summary>
        /// Add Target
        /// </summary>
        public void AddTarget(ITarget target)
        {
            Targets.Add(target);
            onAddTarget(target);
            CountTargets++;
        }

        /// <summary>
        /// Remove Target
        /// </summary>
        public void RemoveTarget(ITarget target)
        {
            Targets.Remove(target);
            onRemoveTarget(target);
            CountTargets--;
        }
    }
}