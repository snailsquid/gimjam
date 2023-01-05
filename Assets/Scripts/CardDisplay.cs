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
    }
    
    void OnMouseOver() {
        Debug.Log("Hello!");
        GameHandler.GetComponent<Game>().hoveredOnCard = transform.gameObject;
    }

    void OnMouseExit() {
        GameHandler.GetComponent<Game>().hoveredOnCard = null;
    }
}
