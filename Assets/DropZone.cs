using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public ZoneTypeEnum zoneType;

    void Start()
    {
        zoneType = ZoneTypeEnum.None;
    }

    void Update()
    {

        List<GameObject> children = Utils.GetAllChildrenGameObjectsFromGameObject(transform.gameObject);
        List<Card> cardList = sortChildren(getCardList(children));

        if (children.Count < 2)
        {
            zoneType = ZoneTypeEnum.None;
            return;
        }

        //To understand the ZoneType i just need the first 2 cards
        if (cardList[0].seed == cardList[1].seed)
        {
            zoneType = ZoneTypeEnum.Ladder;
        }

        if (cardList[0].seed != cardList[1].seed)
        {
            zoneType = ZoneTypeEnum.Poker;
        }
    }

    private List<Card> sortChildren(List<Card> cardList)
    {

        return cardList;
    }

    private List<Card> getCardList(List<GameObject> children)
    {
        List < Card > cardList = new List<Card>();
        foreach (GameObject child in children)
        {
            cardList.Add(child.GetComponent<Card>());
        }
        return cardList;
    }
}
