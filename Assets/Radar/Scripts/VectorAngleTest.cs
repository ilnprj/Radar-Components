using UnityEngine;
using UnityEngine.UI;

public class VectorAngleTest : MonoBehaviour
{

    public Transform player;
    public Transform target;

    public RectTransform arrow; // изображение UI, указатель цели, дочерний объект фона
    public RectTransform compassBG; // изображение UI, фон компаса, в пределах которого будет двигаться указатель

    public Color arrowIn = Color.white;
    public Color arrowOut = Color.gray;

    private float minSize;
    private float maxSize;

    void Start()
    {
        arrow.anchoredPosition = new Vector2(0, 0);
        maxSize = arrow.sizeDelta.x;
        minSize = maxSize / 2;
    }

    void LateUpdate()
    {
        float posX = Camera.main.WorldToScreenPoint(target.position).x; // находим позицию цели в пространстве экрана, по оси Х
        float center = Screen.width / 2; // определяем центр экрана

        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = target.position - Camera.main.transform.position;
        if (Vector3.Dot(forward, toOther) < 0) posX = 0; // если цель позади нас - позиция равна нулю

        float minPos = center - compassBG.sizeDelta.x / 2;
        float maxPos = center + compassBG.sizeDelta.x / 2;
        posX = Mathf.Clamp(posX, minPos, maxPos); // фиксируем позицию цели в приделах бэкграунда компаса

        posX = center - posX; // корректируем позицию, относительно центра
        arrow.anchoredPosition = new Vector2(-posX, 0); // инвертируем

        Color tmp = Color.Lerp(arrowIn, arrowOut, Mathf.Abs(posX) / (compassBG.sizeDelta.x / 2));
        arrow.GetComponent<Image>().color = tmp; // переключаем цвета, значения от 0 до 1, центр экрана = 0

        // определяем размер указателя, относительно расстояния до цели
        float dis = Vector3.Distance(player.position, target.position);
        float size = maxSize - dis / 4;
        size = Mathf.Clamp(size, minSize, maxSize);
        arrow.sizeDelta = new Vector2(size, arrow.sizeDelta.y);
    }
}