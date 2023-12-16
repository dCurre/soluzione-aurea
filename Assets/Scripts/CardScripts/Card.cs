using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    public int value;
    public Seed seed;

    public void Update()
    {
        if(transform.parent != null)
        {
            //I need to keep the card rotated as the parent rotation
            transform.eulerAngles = transform.parent.eulerAngles;
        }
    }
}