using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Контракт для работы классов - цель
    /// </summary>
    public interface ITarget
    {
        Sprite SpriteTarget { get; }

        void OnTargetEnable();

        void OnTargetDisable();
    }
}

