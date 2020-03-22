using UnityEngine;

namespace RadarComponents
{
    public class ContainerTargetManager : MonoBehaviour
    {
        public static ITargetManager targetManager;

        private void Awake()
        {
            try
            {
                targetManager = GetComponent<ITargetManager>();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Необходимо накинуть компонент реализовывающий ITargetManager!");
                Debug.LogError(e.Message);
            }
        }
    }
}