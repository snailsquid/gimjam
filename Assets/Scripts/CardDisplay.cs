using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour
{
    public int id;

    public Text nameText;
    public Text typeText;
    public Text powerText;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = CardDatabase.cardList[id].cardName;
        typeText.text = CardDatabase.cardList[id].type ;
        powerText.text = " " + CardDatabase.cardList[id].power ;

    }
}
