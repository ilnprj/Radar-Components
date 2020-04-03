using UnityEngine.UI;
using UnityEngine;

namespace RadarComponents
{
    public class LineRaycast : MonoBehaviour
    {
        private Vector3 dir = default;

        void Update()
        {
            dir = transform.TransformDirection(Vector3.right);
            Debug.DrawRay(transform.position, dir * 300f, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 300f);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<TweenAlphaImage>().StartLerp();
            }
        }
    }
}