using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    public static List<int> Decide(int health, List<int> hand)
    {
        List<int> HandCopy = new List<int>(hand);
        List<int> preferredCard = new();
        if (health > 20)
        {
            foreach (int cardId in hand)
            {
                if (CardDatabase.cardList[cardId].type == "Attack")
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
                else if (CardDatabase.cardList[cardId].type == "Powerup" && (preferredCard.Count > 0 && CardDatabase.cardList[preferredCard[preferredCard.Count - 1]].type != "Powerup"))
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
            }
            preferredCard.AddRange(HandCopy);
        }
        else if (health <= 20 && health >= 10)
        {
            foreach (int cardId in hand)
            {
                if (CardDatabase.cardList[cardId].type == "Attack")
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
                else if (CardDatabase.cardList[cardId].type == "Defense")
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
                else if (CardDatabase.cardList[cardId].type == "Powerup" && (preferredCard.Count > 0 && CardDatabase.cardList[preferredCard[preferredCard.Count - 1]].type != "Powerup"))
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
            }
            preferredCard.AddRange(HandCopy);
        }
        else
        {
            foreach (int cardId in hand)
            {
                if (CardDatabase.cardList[cardId].type == "Defense")
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
                else if (CardDatabase.cardList[cardId].type == "Health")
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
                else if (CardDatabase.cardList[cardId].type == "Powerup" && (preferredCard.Count > 0 && CardDatabase.cardList[preferredCard[preferredCard.Count - 1]].type != "Powerup"))
                {
                    preferredCard.Add(cardId);
                    HandCopy.Remove(cardId);
                }
            }
            preferredCard.AddRange(HandCopy);
        }
        return preferredCard;
    }
}
