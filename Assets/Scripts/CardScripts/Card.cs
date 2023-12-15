using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    public Seed seed;
}

public enum Seed
{
    Red,
    Blue,
    Green,
    Yellow,
}