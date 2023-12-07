using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        dropZone = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
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
            dropZone = getLastDropZoneChild(dropZone);
            transform.SetParent(dropZone.transform, false);
        }
        else
        {
            transform.position = startPosition;
        }
    }

    private GameObject getLastDropZoneChild(GameObject dropZone)
    {
        while (dropZone.transform.childCount > 0)
        {
            dropZone = dropZone.transform.GetChild(0).gameObject;
        }

        return dropZone;
    }
}
