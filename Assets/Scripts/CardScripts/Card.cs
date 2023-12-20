using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    public int value;
    public Seed seed;
    private bool isDrawn = false;
    private Vector3 scaleChange = new Vector3(10, 10, 10.01f);

    public Card(int value, Seed seed)
    {
        this.value = value;
        this.seed = seed;
    }

    public void Update()
    {
        if(transform.parent != null)
        {
            //I need to keep the card rotated as the parent rotation
            transform.eulerAngles = transform.parent.eulerAngles;
        }
    }

    public void setDrawn(bool isDrawn)
    {
        this.isDrawn = isDrawn;
        setCardImage();
        setColor();
    }

    private void setCardImage()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite =
            !isDrawn ? Resources.Load<Sprite>("CardImages/CardSpriteBack") : Resources.Load<Sprite>("CardImages/CardSprite0" + value);
    }

    private void setColor()
    {
        Seed toCheck = isDrawn ? seed : Seed.Back;

        gameObject.GetComponent<UnityEngine.UI.Image>().color =
            toCheck switch {
                Seed.Red => Color.red,
                Seed.Green => Color.green,
                Seed.Blue => Color.blue,
                Seed.Yellow => Color.yellow,
                _ => Color.white
        };
    }
}