using UnityEngine;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// Контракт для работы классов - цель
    /// </summary>
    public interface ITarget
    {
        string IdTarget { get; }
        Sprite DefaultIcon { get; }
        List<TypeIconTarget> IconsTarget { get; }
        Transform TransformTarget { get;  }

        void OnTargetEnable();

        void OnTargetDisable();
    }
}

