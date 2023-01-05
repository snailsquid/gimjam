using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public int id;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI powerText;
    


    // Start is called before the first frame update
    void Start()
    {
        refresh();
    }

    //Refresh the texts
    void refresh()
    {
        nameText.text = CardDatabase.cardList[id].cardName;
        typeText.text = CardDatabase.cardList[id].type;
        powerText.text = " " + CardDatabase.cardList[id].power;
    }
    
    //Update the Id of the card
    public void updateId(int Id)
    {
        id = Id;
        refresh();
    }
}
