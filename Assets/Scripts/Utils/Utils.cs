using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static bool _debug = true;

    public static List<GameObject> GetAllChildrenGameObjectsFromGameObject(GameObject gameObject)
    {
        List<GameObject> children = new List<GameObject>();

        if (gameObject == null)
        {
            return null;
        }

        while (gameObject.transform.childCount > 0)
        {
            children.Add(gameObject.transform.GetChild(0).gameObject);
            gameObject = gameObject.transform.GetChild(0).gameObject;
        }

        return children;
    }

    public static GameObject GetFirstGameObjectParent(GameObject gameObject, string tagToStop)
    {
        if (gameObject == null)
        {
            return null;
        }

        if(gameObject.transform.parent == null)
        {
            return gameObject;
        }

        while (gameObject.transform.parent != null)
        {
            //I can only get Cards or Dropzones by this method
            if (gameObject.transform.parent.gameObject.transform.tag != TagConstants.Dropzone && gameObject.transform.parent.gameObject.transform.tag != TagConstants.Card)
            {
                return gameObject;
            }

            if (gameObject.transform.parent.gameObject.transform.tag == tagToStop)
            {
                return gameObject.transform.parent.gameObject;
            }

            gameObject = gameObject.transform.parent.gameObject;
        }

        return gameObject;
    }

    public static GameObject getLastDropZoneChild(GameObject dropZone)
    {
        while (dropZone.transform.childCount > 0)
        {
            dropZone = dropZone.transform.GetChild(0).gameObject;
        }
        return dropZone;
    }
}
