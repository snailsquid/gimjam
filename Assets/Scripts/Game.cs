using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject CardTemplate;
    public List<GameObject> Hand = new();
    public int drawAmount;
    public int cardGap;
    public int cardOffset;
    public int cardGapHover;
    public int cardOffsetHover;
    public GameObject hoveredOnCard = null;

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (hoveredOnCard != null){
            if ((Hand.Count)%2 == 0){
                for (int i=0; i<Hand.Count; i++){
                    Hand[i].transform.position = Vector3.Lerp(Hand[i].transform.position, new Vector3(((Hand.Count)/2)*-cardGapHover + (cardGapHover*i) + cardOffsetHover, -15, 10 - (i*0.01f)), 0.1f);
                }
            }
        }else{
            if ((Hand.Count)%2 == 0){
                for (int i=0; i<Hand.Count; i++){
                    Hand[i].transform.position = Vector3.Lerp(Hand[i].transform.position, new Vector3(((drawAmount)/2)*-cardGap + (cardGap*i) + cardOffset, -15, 10 - (i*0.01f)), 0.1f);
                }
            }
        }*/
    }

    public void startGame()
    {
        for (int i=0; i<drawAmount; i++){
            int id = Random.Range(1, CardDatabase.cardList.Count - 1);
            GameObject Temp = Instantiate(CardTemplate, new Vector3(drawAmount/2*-cardGap + (cardGap*i) + cardOffset, -15, 10 - (i*0.01f)), CardTemplate.transform.rotation, CardTemplate.transform.parent);
            Temp.GetComponent<CardDisplay>().id = id;
            Hand.Add(Temp);
        }
    }
}
