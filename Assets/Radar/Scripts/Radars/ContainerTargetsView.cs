using UnityEngine;
using System.Collections.Generic;

namespace RadarComponents
{
    public class ContainerTargetsView : MonoBehaviour
    {
        [Header("Current Targets View")]
        public List<BaseTargetView> Targets = new List<BaseTargetView>();
        [Header("Prefab View Item")]
        [SerializeField]
        private BaseTargetView prefabView;
        [Header("Spawn root")]
        [SerializeField]
        private Transform spawnRoot;
        [Header("Rect root")]
        [SerializeField]
        private RectTransform inputRect;

        private ITargetManager targetManager;

        public PlayerLocator locator;

        /// <summary>
        /// Инициализация контейнера с View. Здесь получаем все цели из TargetManager'a и создаем их отображение на компасе
        /// </summary>
        /// <param name="inputTargetManager"></param>
        public void SetTargetManager(ITargetManager inputTargetManager)
        {
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

        private void OnDisable()
        {
            targetManager.onAddTarget -= onAddTarget;
            targetManager.onRemoveTarget -= onRemoveTarget;
        }

        private void onAddTarget(ITarget target)
        {
            //TODO: Если в пуле элементов свободных нет то заспаунить
            BaseTargetView item = Instantiate(prefabView, spawnRoot);
            item.transform.SetParent(spawnRoot);
            item.InitTargetView(target, locator.transform, inputRect);
            Targets.Add(item);
        }

        private void onRemoveTarget(ITarget target)
        {
            BaseTargetView item = Targets.Find(x => x.Target.IdTarget == target.IdTarget);
            item.gameObject.SetActive(false);
           //TODO: Выкинуть в pool
        }
    }
}
