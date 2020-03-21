namespace RadarComponents
{
    /// <summary>
    /// Контракты для работы таргет менеджера
    /// </summary>
    public interface ITargetManager
    {
        void AddTarget(ITarget target);

        void RemoveTarget(ITarget target);
    }
}
