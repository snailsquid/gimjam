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

    public Material Material;

    private static string defaultImgPath = "Images/placeholder";

    //Refresh the texts
    void Update()
    {
        // nameText.text = CardDatabase.cardList[id].cardName;
        // typeText.text = CardDatabase.cardList[id].type;
        // powerText.text = " " + CardDatabase.cardList[id].power;

        //gameObject.transform.Find("face").GetComponent<Renderer>().material.mainTexture = Resources.Load(CardDatabase.cardList[id].imgPath == "" ? defaultImgPath : CardDatabase.cardList[id].imgPath) as Texture; ;

        gameObject.name = CardDatabase.cardList[id].cardName;
    }
}
