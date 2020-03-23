using UnityEngine;

namespace RadarComponents
{
    /// <summary>
    /// Круговой радар
    /// </summary>
    public class CircleRadar : AbstractRadar
    {
        public override void OnUpdateRadar()
        {
            transform.rotation = Quaternion.Euler(0, 0, locator.transform.localEulerAngles.y);
        }
    }
}

