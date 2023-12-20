using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static List<GameObject> GetAllChildrenGameObjectsFromGameObject(Transform transform)
    {
        if (transform == null)
        {
            return null;
        }

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).gameObject);
        }

        return list;
    }

    public static GameObject getDropZoneParent(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return null;
        }

        if(gameObject.transform.parent == null || gameObject.transform.tag == TagConstants.Dropzone || gameObject.transform.tag == TagConstants.Player)
        {
            return gameObject;
        }

        return gameObject.transform.parent.gameObject;
    }

    public static Transform sortCard(Transform dropZoneTransform)
    {
        Dictionary<GameObject, Card> toSort = new Dictionary<GameObject, Card>();

        foreach(GameObject child in GetAllChildrenGameObjectsFromGameObject(dropZoneTransform))
        {
            toSort.Add(child, child.GetComponent<Card>());
        }

        toSort = (from elem in toSort orderby elem.Value.value ascending select elem).ToDictionary(pair => pair.Key, pair => pair.Value);

        foreach(GameObject child in toSort.Keys)
        {
            child.transform.SetAsLastSibling();
        }

        return dropZoneTransform;
    }

    public static GameObject getLastChild(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return null;
        }

        while(gameObject.transform.childCount > 0)
        {
            gameObject = gameObject.transform.GetChild(0).gameObject;
        }

        return gameObject;
    }
    public static bool isFieldStable()
    {
        Transform dropZoneAreaTransform = GameObject.Find("DropZoneArea").transform;

        for(int i = 0; i < dropZoneAreaTransform.childCount; i++)
        {
            if (!isDropZoneStable(dropZoneAreaTransform.GetChild(i).gameObject))
            {
                GameObject.Find("ErrorMessage").GetComponent<ErrorMessage>().setTextAndShow("Table not stable");
                return false;
            }
        }

        return true;
    }
    private static bool isDropZoneStable(GameObject dropZone)
    {
        /* REGOLE:
         *  1. The dropZone can be empty
         *  2. If not empty, atleast 3 cards
         */
        return dropZone.transform.childCount == 0 || Utils.GetAllChildrenGameObjectsFromGameObject(dropZone.transform).Count > 2;
    }
}
