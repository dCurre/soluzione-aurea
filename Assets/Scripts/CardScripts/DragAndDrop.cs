using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject Canvas;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private Vector2 startPosition;
    private Card draggedCardScript;
    private GameObject startParent;
    private GameObject dropZone;

    private void Awake()
    {
        Canvas = GameObject.Find("GameCanvas");
    }
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
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
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        draggedCardScript = transform.GetComponent<Card>();
        isDragging = true;
    }
    public void EndDrag()
    {
        isDragging = false;

        if (!isOverDropZone)
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
            return;
        }

        dropZone = getLastDropZoneChild(dropZone);
        transform.SetParent(dropZone.transform, false);
    }

    private GameObject getLastDropZoneChild(GameObject dropZone)
    {
        while (dropZone.transform.childCount > 0)
        {
            dropZone = dropZone.transform.GetChild(0).gameObject;
        }

        return dropZone;
    }

    private bool isDropZoneValidCard(GameObject dropZone)
    {
        if(dropZone.transform.GetComponent<Card>() == null)
        {
            return true;
        }

        Card dropZoneCardScript = dropZone.transform.GetComponent<Card>();

        return (draggedCardScript.value == (dropZoneCardScript.value + 1)
                || draggedCardScript.value == (dropZoneCardScript.value - 1));
    }
}
