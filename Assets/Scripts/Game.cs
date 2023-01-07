using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject CardTemplate;
    public List<GameObject> Hand = new();
    public int drawAmount;
    public float cardGap;
    public float cardOffset;
    public GameObject database;

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hand.Count % 2 == 0)
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].transform.position = new Vector3((Hand.Count / 2 * -cardGap + (cardGap * i) + cardOffset) * 1.0f + (cardGap / 2.0f), -15, 10 - (i * 0.01f));
            }
        }
        else
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].transform.position = new Vector3((Hand.Count - 1) / 2 * -cardGap + (cardGap * i) + cardOffset, -15, 10 - (i * 0.01f));
            }
        }

    }

    public void startGame()
    {
        drawCard(drawAmount);
    }

    public void drawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, database.GetComponent<Deck>().deck.Count);
            int id = database.GetComponent<Deck>().deck[index];
            GameObject Temp = Instantiate(CardTemplate, CardTemplate.transform.position, CardTemplate.transform.rotation, CardTemplate.transform.parent);
            Temp.GetComponent<CardDisplay>().id = id;
            Temp.GetComponent<CardDisplay>().order = i;
            Hand.Add(Temp);
        }
    }

    public void removeFromHand(GameObject card)
    {
        Hand.Remove(card);
    }

    public void addToHand(GameObject card)
    {
        Hand.Add(card);
    }
}
