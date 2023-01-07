using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
[System.Serializable]

public class CardDisplay : MonoBehaviour
{
    public int id, order;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI powerText;
    
    public GameObject GameHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Refresh the texts
    void Update()
    {
        nameText.text = CardDatabase.cardList[id].cardName;
        typeText.text = CardDatabase.cardList[id].type;
        powerText.text = " " + CardDatabase.cardList[id].power;

        gameObject.name = CardDatabase.cardList[id].cardName;
    }
}
