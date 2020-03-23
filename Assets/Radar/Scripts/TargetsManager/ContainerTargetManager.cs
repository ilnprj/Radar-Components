using UnityEngine;
using System;

namespace RadarComponents
{
    /// <summary>
    /// Контейнер хранящий в себе реализацию ITargetManager. 
    /// Вообще это нужно сделать через инъекцию
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

        //NOTE: Из-за того что сначала идет определение интерфейса через GetComponent - все таргеты не успевают получить экземпляр, поэтому нужен статус IsInited
        private void Start()
        {
            if (IsInited)
            onInit();
        }
    }
}