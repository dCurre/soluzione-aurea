using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject ActivePlayer;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private Card draggedCardScript;
    private Vector2 pickUpCardPosition;
    private GameObject pickUpCardZone;
    private GameObject dropZone;

    private void Awake()
    {
        Canvas = GameObject.Find("GameCanvas");
        ActivePlayer = GameObject.Find("ActivePlayer");
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
        pickUpCardZone = transform.parent.gameObject;
        pickUpCardPosition = transform.position;
        draggedCardScript = transform.GetComponent<Card>();
        isDragging =
            (pickUpCardZone.tag == TagConstants.Player && pickUpCardZone == ActivePlayer.GetComponent<ActivePlayer>().getActivePlayer())
            || (pickUpCardZone.tag != TagConstants.Player);
    }
    public void EndDrag()
    {
        isDragging = false;

        if (!isOverDropZone || !isValidDropzone(dropZone))
        {
            transform.position = pickUpCardPosition;
            transform.SetParent(pickUpCardZone.transform, false);
            return;
        }

        dropZone = Utils.getLastDropZoneChild(dropZone);
        transform.SetParent(dropZone.transform, false);
    }

    private bool isValidDropzone(GameObject dropObject)
    {
        /* A valid dropzone is:
         *  1. A dropzone without child
         *  2. Same seed different value
         *  3. Different seed same value
         */

        if(dropObject.transform.tag == TagConstants.Dropzone && dropObject.transform.childCount == 0)
        {
            return true;
        }

        GameObject dropZone = Utils.GetFirstGameObjectParent(dropObject, TagConstants.Dropzone);
        DropZone dropZoneScript = dropZone.GetComponent<DropZone>();

        List<GameObject> children = Utils.GetAllChildrenGameObjectsFromGameObject(dropZone);

        foreach (GameObject child in children)
        {
            Card childCardScript = child.GetComponent<Card>();

            if(childCardScript != null)
            {
                if(Rules.isCardSameValueDifferentSeed(draggedCardScript, childCardScript, dropZoneScript))
                {
                    return true;
                }

                if(Rules.isCardNextValueSameSeed(draggedCardScript, childCardScript, dropZoneScript))
                {
                    return true;
                }

                if(Rules.isCardPreviousValueSameSeed(draggedCardScript, childCardScript, dropZoneScript))
                {
                    return true;
                }
            }
        }

        return false;
    }

}
