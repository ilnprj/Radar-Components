// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using UnityEngine;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// Interface for the implementation of the basic functions of the target
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

