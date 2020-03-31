using UnityEngine.UI;
using UnityEngine;

public class SimpleMiniMap : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Image background;

    [SerializeField]
    private GameObject targetViewPrefab;

    public float insideRadarDistance = 10;
    public float blipSizePercentage = 5;

    private float radarWidth;
    private float radarHeight;
    private float blipHeight;
    private float blipWidth;

    private GameObject targetView;
    RectTransform rt;
    private void Awake()
    {
        background = GetComponent<Image>();

        radarWidth = background.rectTransform.rect.width;
        radarHeight = background.rectTransform.rect.height;
        blipHeight = radarHeight * blipSizePercentage / 100;
        blipWidth = radarWidth * blipSizePercentage / 100;
    }

    private void FixedUpdate()
    {
        Calculate();
    }

    private void Calculate()
    {
        Vector3 playerPos = player.position;
        Vector3 targetPos = target.transform.position;
        Vector3 normalisedTargetPosiiton = NormalisedPosition(playerPos, targetPos);
        Vector2 blipPosition = CalculateBlipPosition(normalisedTargetPosiiton);
        blipPosition.x = CheckBorder(blipPosition.x, background.rectTransform.rect.width);
        blipPosition.y = CheckBorder(blipPosition.y, background.rectTransform.rect.height);
        UpdateTargetView(blipPosition);
    }

    private float CheckBorder(float position, float border)
    {
        if (position + blipWidth > border)
        {
            position = border-blipWidth;
        }

        if (position < 0)
        {
            position = 0;
        }
        return position;
    }

    private void UpdateTargetView(Vector2 position)
    {
        if (!targetView)
        {
            targetView = Instantiate(targetViewPrefab, background.rectTransform);
            rt = targetView.GetComponent<RectTransform>();
        }

        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, position.x, blipWidth);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, position.y, blipHeight);
    }

    private Vector3 NormalisedPosition(Vector3 playerPos, Vector3 targetPos)
    {
        float normalisedyTargetX = (targetPos.x - playerPos.x) / insideRadarDistance;
        float normalisedyTargetZ = (targetPos.z - playerPos.z) / insideRadarDistance;
        return new Vector3(normalisedyTargetX, 0, normalisedyTargetZ);
    }

    private Vector2 CalculateBlipPosition(Vector3 targetPos)
    {
        // find angle from player to target
        float angleToTarget = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;

        // direction player facing
        float anglePlayer = player.eulerAngles.y;

        // subtract player angle, to get relative angle to object
        // subtract 90
        // (so 0 degrees (same direction as player) is UP)
        float angleRadarDegrees = angleToTarget - anglePlayer - 90;

        // calculate (x,y) position given angle and distance
        float normalisedDistanceToTarget = targetPos.magnitude;
        float angleRadians = angleRadarDegrees * Mathf.Deg2Rad;
        float blipX = normalisedDistanceToTarget * Mathf.Cos(angleRadians);
        float blipY = normalisedDistanceToTarget * Mathf.Sin(angleRadians);

        // scale blip position according to radar size
        blipX *= radarWidth / 2;
        blipY *= radarHeight / 2;

        // offset blip position relative to radar center
        blipX += radarWidth / 2;
        blipY += radarHeight / 2;

        return new Vector2(blipX, blipY);
    }
}
