using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.RestService;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using TMPro;

public class Game : MonoBehaviour
{
    //* (Without Anything)/P = Player
    //* E = Enemy

    public List<GameObject> Hand = new();
    public List<int> EHand = new();
    public int drawAmount;
    public float cardGap;
    public float cardOffset;
    public GameObject database, handHolder;

    public GameObject PLoc1, PLoc2, ELoc1, ELoc2, CardTemplate;
    public string playScene;
    private List<GameObject> PlayerLoc;
    private List<GameObject> EnemyLoc;
    public Camera PickCam, PlayCam;

    public List<GameObject> pickedCards = new();
    public List<GameObject> EPickedCards = new();
    public List<GameObject> Table = new();

    public bool same = true;
    public GameObject RaycastHolder, pickCardsHolder;
    public GameObject PickedLocation1, PickedLocation2;
    private GameObject[] PickedLocations;

    public int maxHealth = 20;

    public int playerHealth;
    public int EnemyHealth;

    public Slider pHealthBar;
    public Slider eHealthBar;
    public TextMeshProUGUI pHealthText;
    public TextMeshProUGUI eHealthText;

    public bool playing;
    public TextMeshProUGUI playButtonText;

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        Hand.Clear();
        foreach (Transform child in handHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Hand.Add(child.gameObject);
            }
        }
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
        List<GameObject> _pickedCards = RaycastHolder.GetComponent<RaycastSelect>().pickedCards;
        if (pickedCards.Any())
        {
            if (pickedCards.Count == _pickedCards.Count)
            {
                for (int i = 0; i < _pickedCards.Count; i++)
                {
                    if (_pickedCards[i] != pickedCards[i])
                    {

                        same = false;
                        break;
                    }
                }
            }
            else
            {
                same = false;
            }
        }
        else
        {
            if (_pickedCards.Any())
            {
                same = false;
            }
        }

        if (!same)
        {
            Debug.Log("dif");
            foreach (Transform child in pickCardsHolder.transform)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < _pickedCards.Count; i++)
            {
                GameObject Temp = Instantiate(_pickedCards[i], PickedLocations[i].transform.position, _pickedCards[i].transform.rotation);
                Temp.SetActive(true);
                Temp.transform.parent = pickCardsHolder.transform;
                foreach (Transform child in Temp.transform)
                {
                    if(i==0)
                    {
                        child.GetComponent<Animator>().Play("PutInPicked", 0, 0.0f);
                    }
                    else
                    {
                        child.GetComponent<Animator>().Play("PutInPicked2", 0, 0.0f);

                    }
                    child.localPosition = new Vector3(0, 0, 0);
                    child.tag = "Picked";
                }
            }
            pickedCards = new List<GameObject>(_pickedCards);
            same = true;
        }
        pHealthText.text = playerHealth + "/" + maxHealth;
        eHealthText.text = EnemyHealth + "/" + maxHealth;
        pHealthBar.value = playerHealth;
        eHealthBar.value = EnemyHealth;

        playButtonText.text = (playing ? "Continue" : "Play");

        Mathf.Clamp(playerHealth, 0, 20);
        Mathf.Clamp(EnemyHealth, 0, 20);
    }

    public void playPicked()
    {
        if (pickedCards.Count == 2)
        {
            playing = true;
            PickCam.gameObject.SetActive(false);
            PlayCam.gameObject.SetActive(true);

            int atk = 0;
            int def = 0;
            int health = 0;

            int Eatk = 0;
            int Edef = 0;
            int Ehealth = 0;

            for (int i = 0; i < pickedCards.Count; i++)
            {
                GameObject Temp = Instantiate(pickedCards[i], PlayerLoc[i].transform.position, PlayerLoc[i].transform.rotation);
                Temp.SetActive(true);
                Temp.tag = "Playing";
                foreach (Transform child in Temp.transform)
                {
                    child.tag = "Playing";
                }
                int id = Temp.GetComponent<CardDisplay>().id;
                string type = CardDatabase.cardList[id].type;
                int power = CardDatabase.cardList[id].power;
                Table.Add(Temp);
                if (type == "Attack")
                {
                    atk = atk + power;
                }
                else if (type == "Defense")
                {
                    def = def + power;
                }
                else if (type == "Health")
                {
                    health = health + power;
                }
            }

            for (int i = 0; i < Hand.Count; i++)
            {
                GameObject temp = Hand[i];
                if (temp.activeSelf == false)
                {
                    removeFromHand(temp);
                    Destroy(temp);
                }
            }

            // Ai for now only pick random cards
            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, EHand.Count);
                int id = EHand[index];
                removeFromEnemyHand(id);
                GameObject ETemp = Instantiate(CardTemplate, EnemyLoc[i].transform.position, EnemyLoc[i].transform.rotation, CardTemplate.transform.parent);
                ETemp.GetComponent<CardDisplay>().id = id;
                ETemp.GetComponent<CardDisplay>().order = i;
                foreach(Transform child in ETemp.transform)
                {
                    child.tag = "Enemy";
                }
                EPickedCards.Add(ETemp);
                string type = CardDatabase.cardList[id].type;
                int power = CardDatabase.cardList[id].power;
                if (type == "Attack")
                {
                    Eatk = Eatk + power;
                }
                else if (type == "Defense")
                {
                    Edef = Edef + power;
                }
                else if (type == "Health")
                {
                    Ehealth = Ehealth + power;
                }
            }

            Debug.Log(atk);
            Debug.Log(def);
            Debug.Log(health);

            Debug.Log(Eatk);
            Debug.Log(Edef);
            Debug.Log(Ehealth);

            int dmg = atk - Edef;
            if (dmg < 0)
            {
                dmg = 0;
            }
            int Eheal = Ehealth - dmg;

            int Edmg = Eatk - def;
            if (Edmg < 0)
            {
                Edmg = 0;
            }
            int heal = health - Edmg;

            playerHealth += heal;
            EnemyHealth += Eheal;

        }
    }

    public void play()
    {
        if (playing)
        {
            nextRound();
        }
        else
        {
            playPicked();
        }
    }

    //kiigo was here
    public void startGame()
    {
        PickedLocations = new GameObject[2] { PickedLocation1, PickedLocation2 };
        PlayCam.gameObject.SetActive(false);
        PickCam.gameObject.SetActive(true);

        EnemyLoc = new List<GameObject> { ELoc1, ELoc2 };
        PlayerLoc = new List<GameObject> { PLoc1, PLoc2 };

        playerHealth = maxHealth;
        EnemyHealth = maxHealth;

        drawCard(drawAmount);
        drawToEnemy(drawAmount);
    }

    public void nextRound()
    {
        playing = false;
        PlayCam.gameObject.SetActive(false);
        PickCam.gameObject.SetActive(true);

        for (int i = 0; i < pickedCards.Count; i++)
        {
            Destroy(pickedCards[i]);
        }
        pickedCards.Clear();

        for (int i = 0; i < EPickedCards.Count; i++)
        {
            Destroy(EPickedCards[i]);
        }
        EPickedCards.Clear();
        RaycastHolder.GetComponent<RaycastSelect>().pickedCards.Clear();

        for (int i = 0; i < Table.Count; i++)
        {
            Destroy(Table[i]);
        }
        EPickedCards.Clear();

        for (int i = pickCardsHolder.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(pickCardsHolder.transform.GetChild(i).gameObject);
        }

        drawCard(2);
        drawToEnemy(2);
    }

    public void drawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(1, database.GetComponent<Deck>().deck.Count);
            int id = database.GetComponent<Deck>().deck[index];
            GameObject Temp = Instantiate(CardTemplate, CardTemplate.transform.position, CardTemplate.transform.rotation, handHolder.transform);
            Temp.GetComponent<CardDisplay>().id = id;
            Temp.GetComponent<CardDisplay>().order = i;
        }
    }

    public void drawToEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(1, database.GetComponent<Deck>().deck.Count);
            int id = database.GetComponent<Deck>().deck[index];
            EHand.Add(id);
        }
    }

    public void removeFromHand(GameObject card)
    {
        Hand.Remove(card);
    }

    public void removeFromEnemyHand(int id)
    {
        EHand.Remove(id);
    }

    public void addToHand(GameObject card)
    {
        Hand.Add(card);
    }

    public void addToEnemyHand(int id)
    {
        EHand.Add(id);
    }

}
