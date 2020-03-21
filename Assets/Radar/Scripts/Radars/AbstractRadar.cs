using UnityEngine;

/// <summary>
/// Абстрактный класс радара. Работает при обновлении локатора игрока
/// </summary>
public abstract class AbstractRadar : MonoBehaviour
{
    protected PlayerLocator locator = null;

    protected virtual void Awake()
    {
        CheckLocator();
    }

    private void CheckLocator()
    {
        if (PlayerLocator.IsInited)
        {
            locator = FindObjectOfType<PlayerLocator>();
        }
        else
        {
            PlayerLocator.onInit += SetLocator;
        }
        PlayerLocator.onUpdateLocator += OnUpdateRadar;
    }

    protected virtual void OnDestroy()
    {
        PlayerLocator.onInit -= SetLocator;
        PlayerLocator.onUpdateLocator -= OnUpdateRadar;
    }

    private void SetLocator()
    {
        locator = FindObjectOfType<PlayerLocator>();
    }

    /// <summary>
    /// Обновить данные радара в его реализации
    /// </summary>
    public abstract void OnUpdateRadar();
}
