// MIT License
// https://gitlab.com/ilnprj 
// Copyright (c) 2020 ilnprj

using UnityEngine;
using System.Collections.Generic;

namespace RadarComponents
{
    /// <summary>
    /// A container that accepts tasks from the manager and sends them to the spawn / pool manager
    /// </summary>
    public class ContainerTargetsView : MonoBehaviour
    {
        [Header("Current Targets View")]
        public List<BaseTargetView> Targets = new List<BaseTargetView>();

        [Header("Prefab View Item")]
        [SerializeField]
        private BaseTargetView prefabView = default;

        [Header("Spawn root")]
        [SerializeField]
        private Transform spawnRoot = default;

        private ITargetManager targetManager;
        private PlayerLocator locator;
        private PoolBaseTargetView pool;


        /// <summary>
        /// Initializing a container with View.Here we get all the targets from TargetManager and create their display on the compass
        /// </summary>
        /// <param name="inputTargetManager"></param>
        public void SetTargetManager(ITargetManager inputTargetManager)
        {
            locator = FindObjectOfType<PlayerLocator>();
            pool = gameObject.AddComponent<PoolBaseTargetView>();
            targetManager = inputTargetManager;
            targetManager.onAddTarget += onAddTarget;
            targetManager.onRemoveTarget += onRemoveTarget;

            if (targetManager.Targets.Count != 0)
            {
                foreach (var item in targetManager.Targets)
                {
                    onAddTarget(item);
                }
            }
        }

        private void OnValidate()
        {
            if (prefabView == null || spawnRoot == null)
            {
                Debug.LogError("Please set prefab and root transform in inspector. GameObject -"+gameObject.name);
            }
        }

        private void OnDisable()
        {
            targetManager.onAddTarget -= onAddTarget;
            targetManager.onRemoveTarget -= onRemoveTarget;
        }

        private void onAddTarget(ITarget target)
        {
            BaseTargetView item = pool.GetNewView(prefabView, spawnRoot);
            RectTransform rect = spawnRoot.GetComponent<RectTransform>();
            item.transform.SetParent(spawnRoot);
            item.InitTargetView(target, locator.transform, rect);
            Targets.Add(item);
        }

        private void onRemoveTarget(ITarget target)
        {
            BaseTargetView item = Targets.Find(x => x.CurrentTarget.GetHashCode() == target.GetHashCode());
            pool.SetToPool(item);
        }
    }
}