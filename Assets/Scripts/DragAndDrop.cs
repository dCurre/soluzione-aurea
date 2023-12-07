using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private Vector2 startPosition;
    private GameObject dropZone;

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = null;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = collision.gameObject;
    }

    public void StartDrag()
    {
        startPosition = transform.position;
        isDragging = true;
    }
    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
        } else
        {
            transform.position = startPosition;
        }
    }
}
