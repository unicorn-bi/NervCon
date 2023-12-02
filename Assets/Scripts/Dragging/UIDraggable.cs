using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable :  MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private DragManager2D _manager = null;

    private Vector2 _centerPoint;
    private Vector2 _worldCenterPoint => transform.TransformPoint(_centerPoint);

    private void Awake()
    {
        _manager = GetComponentInParent<DragManager2D>();
        _centerPoint = (transform as RectTransform).rect.center;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _manager.RegisterDraggedObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_manager.IsWithinBounds(_worldCenterPoint + eventData.delta))
        {
            transform.Translate(eventData.delta);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _manager.UnregisterDraggedObject(this);
    }
}