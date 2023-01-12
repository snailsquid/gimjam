using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.RestService;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using TMPro;
using UnityEditorInternal;

public class Game : MonoBehaviour
{
    //* (Without Anything)/P = Player
    //* E = Enemy

    private int heal, Eheal;

    public GameObject midText;

    public GameObject floatingText;

    public GameObject particleManager;
    public Color green = new(0.7f, 1f, 0.1275f,1);
    public Color red = new(255, 20, 20, 1);

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
    public GameObject PlayPicked;
    private GameObject[] PickedLocations;

    public int maxHealth;

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
    public Sprite cardBackground;

    public GameObject audioManager;
    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    public List<GameObject> _pickedCards;
    private float time;

    private float lerpSpeed = 0.25f;  

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
        else if(_pickedCards.Any())
        {
            same = false;
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
                    midText.SetActive(true);

                    GameObject Temp = Instantiate(_pickedCards[i], PickedLocations[i].transform.position, _pickedCards[i].transform.rotation, pickCardsHolder.transform);
                    Temp.SetActive(true);
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
                        audioManager.GetComponent<AudioManager>().PlaySFX(child.GetComponent<CardDisplay>().id);
                    }
                }
            }
            pickedCards = new List<GameObject>(_pickedCards);
            same = true;
        }


        pHealthText.text = playerHealth/10 + "/" + maxHealth/10;
        eHealthText.text = EnemyHealth/10 + "/" + maxHealth / 10;
        time += Time.deltaTime * lerpSpeed;
        pHealthBar.value = Mathf.Lerp(pHealthBar.value, playerHealth, time);
        eHealthBar.value = Mathf.Lerp(eHealthBar.value, EnemyHealth, time);

        playButtonText.text = (playing ? (Hand.Count > 0 ? "Continue" : "Next Round") : "Play");

        playerHealth = Mathf.Clamp(playerHealth, 0, 300);
        EnemyHealth = Mathf.Clamp(EnemyHealth, 0, 300);
    }

    public void playPicked()
    {
        midText.SetActive(true);
        midText.GetComponent<TextMesh>().text = "VS";
        if (pickedCards.Count == 2)
        {
        foreach (Transform child in pickCardsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform parent in handHolder.transform)
        {
            foreach (Transform child in parent)
            {
                child.tag = "Unplaying";
            }
        }

            playing = true;

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
                    child.GetComponent<Animator>().Play("TransitionToPlay");
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
                time = 0;

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

            List<int> Decision = Ai.Decide(EnemyHealth, EHand);
            for (int i = 0; i < 2; i++)
            {
                int id = Decision[i];
                removeFromEnemyHand(id);
                GameObject ETemp = Instantiate(CardTemplate, EnemyLoc[i].transform.position, EnemyLoc[i].transform.rotation, CardTemplate.transform.parent);
                ETemp.transform.GetChild(0).GetComponent<CardDisplay>().id = id;
                ETemp.transform.GetChild(0).GetComponent<CardDisplay>().order = i;
                ETemp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cardBackground;
                foreach (Transform child in ETemp.transform)
                {
                    child.GetComponent<Animator>().Play("EnemyToPlay");
                    child.tag = "Playing";
                }
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
                time = 0;
            }

            Debug.Log(atk);
            Debug.Log(def);
            Debug.Log(health);

            int dmg = atk - Edef;
            int Edmg = Eatk - def;
            if (dmg < 0)
            {
                Edmg += dmg;
                dmg = 0;
            }
            if (Edmg < 0)
            {
                dmg += Edmg;
                Edmg = 0;
            }
             Eheal = Ehealth - dmg;

             heal = health - Edmg;

            
            
        }
    }

   public void damage()
    {
        if (heal > 0)
        {
            particleManager.GetComponent<ParticleManager>().PlayerSpawn(green);
            floatingText.GetComponent<FloatingText>().PlayerHealth(green, "+" + heal.ToString());
        }
        else if (heal < 0)
        {
            particleManager.GetComponent<ParticleManager>().PlayerSpawn(red);
            floatingText.GetComponent<FloatingText>().PlayerHealth(red, "-" + heal.ToString());
        }
        if (Eheal > 0)
        {
            particleManager.GetComponent<ParticleManager>().EnemySpawn(green);
            floatingText.GetComponent<FloatingText>().EnemyHealth(green, "+" + Eheal.ToString());
        }
        else if (Eheal < 0)
        {
            particleManager.GetComponent<ParticleManager>().EnemySpawn(red);
            floatingText.GetComponent<FloatingText>().EnemyHealth(red, "-" + Eheal.ToString());
        }

        playerHealth += heal * 10;
        EnemyHealth += Eheal * 10;
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
        AudioManager = audioManager.GetComponent<AudioManager>();
        AudioManager.PlayMusic();

        PickedLocations = new GameObject[2] { PickedLocation1, PickedLocation2 };

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
        midText.SetActive(false);
        midText.GetComponent<TextMesh>().text = "+";

        playing = false;
        foreach (Transform parent in handHolder.transform)
        {
            foreach(Transform child in parent)
            {

                child.tag = "Selectable";
            }
        }

        for (int i = 0; i < pickedCards.    Count; i++)
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

