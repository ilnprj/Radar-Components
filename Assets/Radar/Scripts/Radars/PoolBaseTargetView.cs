using System.Collections.Generic;
using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// A pool for target views that can be constantly turned on and off
    /// </summary>
    public class PoolBaseTargetView: MonoBehaviour
    {
        private List<BaseTargetView> poolTargetViews =  new List<BaseTargetView>();

        public BaseTargetView GetNewView(BaseTargetView prefabView, Transform spawnRoot)
        {
            BaseTargetView view;
            if (poolTargetViews.Count <= 0)
            {
                view = Instantiate(prefabView, spawnRoot);
            }
            else
            {
                view = poolTargetViews[poolTargetViews.Count - 1];
                poolTargetViews.Remove(view);
                view.gameObject.SetActive(true);
            }
            return view;
        }

        public void SetToPool(BaseTargetView view)
        {
            view.gameObject.SetActive(false);
            poolTargetViews.Add(view);
        }
    }
}
