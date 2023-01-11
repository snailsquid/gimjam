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
    public Vector3 cardOffset;
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

    public List<int> pDeck = new();
    public List<int> eDeck = new();

    public List<Sprite> cardimages = new();

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    public List<GameObject> _pickedCards;

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
                Hand[i].transform.position = new Vector3((Hand.Count / 2 * -cardGap + (cardGap * i)) * 1.0f + (cardGap / 2.0f), 8);
                Hand[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -i;
            }
        }
        else
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                Hand[i].transform.position = new Vector3((Hand.Count - 1) / 2 * -cardGap + (cardGap * i), 8);
                Hand[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -i;

            }
        }
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
            if (_pickedCards.Count <= 2)
            {

                for (int i = 0; i < _pickedCards.Count; i++)
                {
                    Debug.Log("cccc");
                    GameObject Temp = Instantiate(_pickedCards[i], PickedLocations[i].transform.position, _pickedCards[i].transform.rotation, pickCardsHolder.transform);
                    Temp.SetActive(true);
                    Debug.Log("hhh");
                    foreach (Transform child in Temp.transform)
                    {
                        if (i == 0)
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
            }
            pickedCards = new List<GameObject>(_pickedCards);
            same = true;
        }
        pHealthText.text = playerHealth + "/" + maxHealth;
        eHealthText.text = EnemyHealth + "/" + maxHealth;
        pHealthBar.value = playerHealth;
        eHealthBar.value = EnemyHealth;

        playButtonText.text = (playing ? (Hand.Count > 0 ? "Continue" : "Next Round") : "Play");

        playerHealth = Mathf.Clamp(playerHealth, 0, 30);
        EnemyHealth = Mathf.Clamp(EnemyHealth, 0, 30);
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

            int? pPlayed = null;
            int? ePlayed = null;

            for (int i = 0; i < pickedCards.Count; i++)
            {
                GameObject Temp = Instantiate(pickedCards[i], PlayerLoc[i].transform.position, PlayerLoc[i].transform.rotation);
                Temp.SetActive(true);
                Temp.tag = "Playing";
                foreach (Transform child in Temp.transform)
                {
                    child.tag = "Playing";
                }
                int id = Temp.transform.GetChild(0).GetComponent<CardDisplay>().id;
                string type = CardDatabase.cardList[id].type;
                int power = CardDatabase.cardList[id].power;
                Table.Add(Temp);

                int addition = 0;

                if (i == 0)
                {
                    pPlayed = id;
                }
                else if (i == 1 && pPlayed == id)
                {
                    addition = 4;
                }

                if ((id == 9 && pPlayed == 15) || (id == 15 && pPlayed == 9))
                {
                    addition = 8;
                }

                if ((id == 1 && pPlayed == 17) || (id == 17 && pPlayed == 1))
                {
                    atk += 4;
                    def += 4;
                }

                if (type == "Attack")
                {
                    atk += power + addition;
                }
                else if (type == "Defense")
                {
                    def += power + addition;
                }
                else if (type == "Health")
                {
                    health += power + addition;
                }
                else if (type == "Powerup")
                {
                    if (atk > 0) atk += power;
                    if (def > 0) def += power;
                    if (health > 0) health += power;
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
                ETemp.transform.GetChild(0).GetComponent<CardDisplay>().id = id;
                ETemp.transform.GetChild(0).GetComponent<CardDisplay>().order = i;
                ETemp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cardimages[id];
                foreach (Transform child in ETemp.transform)
                {
                    child.tag = "Enemy";
                }
                EPickedCards.Add(ETemp);
                string type = CardDatabase.cardList[id].type;
                int power = CardDatabase.cardList[id].power;

                int addition = 0;

                if (i == 0)
                {
                    ePlayed = id;
                }
                else if (i == 1 && pPlayed == id)
                {
                    addition = 4;
                }

                if ((id == 9 && pPlayed == 15) || (id == 15 && pPlayed == 9))
                {
                    addition = 8;
                }

                if ((id == 1 && pPlayed == 17) || (id == 17 && pPlayed == 1))
                {
                    Eatk += 4;
                    Edef += 4;
                }

                if (type == "Attack")
                {
                    Eatk += power + addition;
                }
                else if (type == "Defense")
                {
                    Edef += power + addition;
                }
                else if (type == "Health")
                {
                    Ehealth += power + addition;
                }
                else if (type == "Powerup")
                {
                    if (Eatk > 0) Eatk += power;
                    if (Edef > 0) Edef += power;
                    if (Ehealth > 0) Ehealth += power;
                }
            }

            Debug.Log(atk);
            Debug.Log(def);
            Debug.Log(health);

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

    public void startGame()
    {
        PickedLocations = new GameObject[2] { PickedLocation1, PickedLocation2 };
        PlayCam.gameObject.SetActive(false);
        PickCam.gameObject.SetActive(true);

        EnemyLoc = new List<GameObject> { ELoc1, ELoc2 };
        PlayerLoc = new List<GameObject> { PLoc1, PLoc2 };

        pHealthBar.maxValue = maxHealth;
        eHealthBar.maxValue = maxHealth;

        playerHealth = maxHealth;
        EnemyHealth = maxHealth;

        resetEnemyDeck();
        resetPlayerDeck();

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
        _pickedCards.Clear();

        for (int i = 0; i < EPickedCards.Count; i++)
        {
            Destroy(EPickedCards[i]);
        }
        EPickedCards.Clear();

        for (int i = 0; i < Table.Count; i++)
        {
            Destroy(Table[i]);
        }
        EPickedCards.Clear();

        Debug.Log(pickCardsHolder.transform.childCount);

        foreach (Transform child in pickCardsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        if (Hand.Count == 0)
        {
            resetPlayerDeck();
            resetEnemyDeck();
            drawCard(3);
            drawToEnemy(3);
        }

        drawCard(2);
        drawToEnemy(2);
    }

    public void drawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (pDeck.Count > 0)
            {
                int index = Random.Range(0, pDeck.Count);
                int id = pDeck[index];
                GameObject Temp = Instantiate(CardTemplate, handHolder.transform);
                Temp.transform.GetChild(0).GetComponent<CardDisplay>().id = id;
                Temp.transform.GetChild(0).GetComponent<CardDisplay>().order = i;
                Temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cardimages[id];
                pDeck.Remove(id);
            }
        }
    }

    public void drawToEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (eDeck.Count > 0)
            {
                int index = Random.Range(0, eDeck.Count);
                int id = eDeck[index];
                EHand.Add(id);
                eDeck.Remove(id);
            }
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

    public void resetPlayerDeck()
    {
        pDeck.Clear();
        foreach (Card card in CardDatabase.cardList)
        {
            pDeck.Add(card.id);
            pDeck.Add(card.id);
        }
    }

    public void resetEnemyDeck()
    {
        eDeck.Clear();
        foreach (Card card in CardDatabase.cardList)
        {
            eDeck.Add(card.id);
            eDeck.Add(card.id);
        }
    }

}
