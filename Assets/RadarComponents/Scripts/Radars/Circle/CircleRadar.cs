// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj


using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Simple circle radar without items
    /// </summary>
    public class CircleRadar : AbstractRadar
    {
        public override void OnUpdateRadar()
        {
            transform.rotation = Quaternion.Euler(0, 0, locator.transform.localEulerAngles.y);
        }
    }
}

