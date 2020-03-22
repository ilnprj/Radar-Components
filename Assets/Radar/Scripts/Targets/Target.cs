using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Класс цели, регистрирует
    /// </summary>
    public class Target :  MonoBehaviour, ITarget
    {
        //Вот здесь идеальное место для инъекции
        private ITargetManager targetManager;

        [SerializeField]
        private Sprite targetImage;
        public Sprite SpriteTarget { get => targetImage; }

        private void Start()
        {
            targetManager = ContainerTargetManager.TargetManagerContainer;
        }

        private void OnEnable()
        {
            OnTargetEnable();
        }

        private void OnDisable()
        {
            OnTargetDisable();
        }

        public void OnTargetEnable()
        {
            
        }

        public void OnTargetDisable()
        {
           
        }
    }
}
