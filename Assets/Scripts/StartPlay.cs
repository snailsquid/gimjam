using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPlay : MonoBehaviour
{
    public Button startButton;
    public GameObject PickedHolder, PLoc1, PLoc2, ELoc1, ELoc2, database, CardTemplate; //P = Player, E = Enemy
    public string playScene;
    private List<GameObject> PlayerLoc;
    private List<GameObject> EnemyLoc;
    public Camera PickCam, PlayCam;

    public List<GameObject> pickedCards = new();
    public List<GameObject> EPickedCards = new();   

    void PlayStart()
    {
        pickedCards = PickedHolder.GetComponent<PickedCard>().pickedCards;
        PickCam.gameObject.SetActive(false);
        PlayCam.gameObject.SetActive(true);



        for(int i = 0; i<pickedCards.Count; i++)
        {
            GameObject Temp = Instantiate(pickedCards[i], PlayerLoc[i].transform.position, PlayerLoc[i].transform.rotation);
            Temp.SetActive(true);
            Temp.tag = "Playing";

            //enemy
            int index = Random.Range(1, database.GetComponent<Deck>().deck.Count);
            int id = database.GetComponent<Deck>().deck[index];
            GameObject ETemp = Instantiate(CardTemplate, EnemyLoc[i].transform.position, EnemyLoc[i].transform.rotation, CardTemplate.transform.parent);
            ETemp.GetComponent<CardDisplay>().id = id;
            ETemp.GetComponent<CardDisplay>().order = i;
            ETemp.tag = "Enemy";
            EPickedCards.Add(ETemp);
        }
    }

    void Start()
    {
        PlayCam.gameObject.SetActive(false) ;
        PickCam.gameObject.SetActive(true);

        EnemyLoc = new List<GameObject> { ELoc1, ELoc2 };
        PlayerLoc = new List<GameObject>{PLoc1, PLoc2};
        startButton.GetComponent<Button>().onClick.AddListener(PlayStart);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
