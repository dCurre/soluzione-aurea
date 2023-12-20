using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject ActivePlayer;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private Card draggedCardScript;
    private Vector2 pickUpCardPosition;
    private GameObject startingPickupZone;
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

    public void StartDrag()
    {
        startingPickupZone = transform.parent.gameObject;
        pickUpCardPosition = transform.position;
        draggedCardScript = transform.GetComponent<Card>();

        //If the card is in hand of the right player (turn of that player) or is in a dropzone
        isDragging = isDraggingCard();
    }
    public void EndDrag()
    {
        isDragging = false;

        if (!isOverDropZone || !isValidDropzone())
        {
            transform.position = pickUpCardPosition;
            transform.SetParent(startingPickupZone.transform, false);
            return;
        }

        //If dropzone is discardZone i need to drop the cards as a stack
        transform.SetParent(
            (dropZone.tag == TagConstants.DiscardZone) ? Utils.getLastChild(dropZone).transform : dropZone.transform,
            false);

        //Saving move history
        GameHistory.SaveHistory(new Action(transform.gameObject, startingPickupZone, dropZone, GameHistory.currentTurn));
    }

    private bool isValidDropzone()
    {
        /* A valid dropzone is:
         *  1. A dropzone without child
         *  2. Same seed different value
         *  3. Different seed same value
         */

        Transform dropZoneTransform = (dropZone.tag == TagConstants.DiscardZone) ? dropZone.transform : Utils.getDropZoneParent(dropZone).transform;
        DropZone dropZoneScript = dropZone.GetComponent<DropZone>();

        //Cards on discardZone can't be put in hand
        if (dropZoneTransform.tag == TagConstants.Player && (startingPickupZone.tag == TagConstants.DiscardZone || startingPickupZone.tag == TagConstants.Dropzone))
        {
            return false;
        }

        if (dropZoneTransform.tag == TagConstants.DiscardZone
            || dropZoneTransform.tag == TagConstants.Player 
            || (dropZoneTransform.tag == TagConstants.Dropzone && dropZoneTransform.childCount == 0))
        {
            return true;
        }

        foreach (GameObject child in Utils.GetAllChildrenGameObjectsFromGameObject(dropZone.transform))
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

    private bool isDraggingCard()
    {
        if (startingPickupZone.tag == TagConstants.DiscardZone && Utils.isFieldStable())
        {
            return true;
        }
        
        if (startingPickupZone.tag == TagConstants.Player && startingPickupZone == ActivePlayer.GetComponent<ActivePlayer>().getActivePlayer())
        {
            return true;
        }

        if (startingPickupZone.tag != TagConstants.Player && startingPickupZone.tag != TagConstants.DiscardZone)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = collision.gameObject.tag == TagConstants.Card ? Utils.getDropZoneParent(collision.gameObject) : collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

}
