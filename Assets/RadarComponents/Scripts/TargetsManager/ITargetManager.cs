// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using System;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// Interface for Target Manager base functional
    /// </summary>
    public interface ITargetManager
    {
        event Action<ITarget> onAddTarget;
        event Action<ITarget> onRemoveTarget;
        List<ITarget> Targets { get; }

        void AddTarget(ITarget target);

        void RemoveTarget(ITarget target);
    }
}
