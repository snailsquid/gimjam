using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public string type;
    public int power;

    public Card()
    {
        
    }

    public Card(int Id, string CardName, string Type, int Power)
    {
        id = Id;
        cardName = CardName;
        type = Type;
        power = Power;

    }
}
