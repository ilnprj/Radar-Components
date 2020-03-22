using System;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// Контракты для работы таргет менеджера
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
