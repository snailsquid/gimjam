using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new();

    public void Awake()
    {
        cardList.Add(new Card(0, "None", "None", 0));
        cardList.Add(new Card(1, "The Rock", "Attack", 2));
        cardList.Add(new Card(2, "Social Credit", "Health", 1));
        cardList.Add(new Card(3, "Will Slap", "Attack", 2));
        cardList.Add(new Card(4, "America looking for oil", "Attack", 3));
        Debug.Log("hi");
    }
}
