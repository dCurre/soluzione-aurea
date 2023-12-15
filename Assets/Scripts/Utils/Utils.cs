using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static bool _debug = true;

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
}
