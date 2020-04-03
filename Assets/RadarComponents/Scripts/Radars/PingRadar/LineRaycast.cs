using UnityEngine;

namespace RadarComponents
{
    public class LineRaycast : MonoBehaviour
    {
        [SerializeField]
        private Vector3 rotationVector = default;

        [SerializeField]
        private float speed = default;

        [SerializeField]
        private float distanceRay = 150f;

        private void Update()
        {
            transform.Rotate(rotationVector * speed);
            Debug.DrawRay(transform.position, GetVectorFromAngle(transform.eulerAngles.z) * distanceRay, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, GetVectorFromAngle(transform.eulerAngles.z), distanceRay);
            if (hit.collider != null)
            {
                //FIXME: Raycast need one shot, this creates many nasty calls :(
                hit.collider.GetComponent<TweenAlphaImage>().StartLerp();
            }
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
    }
}