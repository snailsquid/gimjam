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
        Debug.Log("hi");
    }
}
