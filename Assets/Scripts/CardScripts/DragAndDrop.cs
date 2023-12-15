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

    [SerializeField]
    private bool isOverDropZone = false;
    private Card draggedCardScript;
    private Vector2 pickUpCardPosition;
    private GameObject zoneToReturn;
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
        //Debug.Log("COLLISION: " + dropZone.name);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        zoneToReturn = transform.parent.gameObject;
        pickUpCardPosition = transform.position;
        draggedCardScript = transform.GetComponent<Card>();

        //If the card is in hand of the right player (turn of that player) or is in a dropzone
        isDragging = 
            (zoneToReturn.tag == TagConstants.Player && zoneToReturn == ActivePlayer.GetComponent<ActivePlayer>().getActivePlayer())
            || (zoneToReturn.tag != TagConstants.Player);
    }
    public void EndDrag()
    {
        isDragging = false;

        if (!isOverDropZone || !isValidDropzone(dropZone))
        {
            transform.position = pickUpCardPosition;
            transform.SetParent(zoneToReturn.transform, false);
            return;
        }

        transform.SetParent(Utils.getDropZoneParent(dropZone).transform, false);
    }

    private bool isValidDropzone(GameObject dropObject)
    {
        /* A valid dropzone is:
         *  1. A dropzone without child
         *  2. Same seed different value
         *  3. Different seed same value
         */

        GameObject dropZoneABC = Utils.getDropZoneParent(dropObject);
        Transform dropZoneTransform = dropZoneABC.transform;
        DropZone dropZoneScript = dropZoneABC.GetComponent<DropZone>();

        if (dropZoneTransform.tag == TagConstants.Player || (dropZoneTransform.tag == TagConstants.Dropzone && dropZoneTransform.childCount == 0))
        {
            return true;
        }

        foreach (GameObject child in Utils.GetAllChildrenGameObjectsFromGameObject(dropZoneABC.transform))
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
