using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorSwitch : MonoBehaviour
    , IDragHandler
    , IEndDragHandler
    , IPointerDownHandler
{
    public bool isDragging { get; private set; }
    public Vector2 inputDelta { get; private set; }
    public Vector2 magnitude { get; private set; }
    private Vector2 origin;
    private Vector3 offset;
    public GameObject text, palette, player;

    void Start()
    {
        origin = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 curPos = eventData.position; //Convert Vector2 eventData to a Vector3
        this.transform.position = curPos + offset;

        inputDelta = eventData.position - origin;
        inputDelta = inputDelta.normalized;

        magnitude = new Vector2(Mathf.Abs(eventData.position.x - origin.x), Mathf.Abs(eventData.position.y - origin.y));

        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = origin;
        inputDelta = Vector2.zero;
        isDragging = false;
        text.SetActive(true);
        palette.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.SetActive(false);
        palette.SetActive(true);
        Vector3 pointerPos = eventData.position;
        this.offset = this.transform.position - pointerPos;
    }
    
    private void OnTriggerEnter2D(Collider2D color)
    {
        ColorChange colorChange = player.GetComponent<ColorChange>();

        if (color.gameObject.tag == "Color")
        {
            colorChange.changeColor(color.gameObject.name);
        }
    }
}
