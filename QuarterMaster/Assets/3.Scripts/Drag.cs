using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera camera;
    [SerializeField] private Vector2 offset;
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            camera,
            out position);

        Vector2 finalPosition = position * offset;
        transform.position = canvas.transform.TransformPoint(finalPosition);

    }
}
