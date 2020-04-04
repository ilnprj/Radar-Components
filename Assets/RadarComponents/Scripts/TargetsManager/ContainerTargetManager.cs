// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using UnityEngine;
using System;

namespace RadarComponents
{
    /// <summary>
    /// Container stores the implementation of ITargetManager.
    /// In general, this must be done through injection
    /// </summary>
    public class ContainerTargetManager : MonoBehaviour
    {
        public ITargetManager TargetManager;
        public bool IsInited;
        public Action onInit = delegate { };

        private void Awake()
        {
            TargetManager = GetComponent<TargetManager>();
            IsInited = true;
        }

        //NOTE: Due to the fact that the interface is first defined via GetComponent, all targets do not have time to receive an instance, therefore, the status IsInited is required
        private void Start()
        {
            if (IsInited)
            onInit();
        }
    }
}