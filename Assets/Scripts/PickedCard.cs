using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PickedCard : MonoBehaviour
{
    public bool same = true;
    public GameObject RaycastHolder, CardTemplate, pickCardsHolder;
    public  List<GameObject> pickedCards;
    public GameObject PickedLocation1, PickedLocation2;
    private GameObject[] PickedLocations;

    // Start is called before the first frame update
    void Start()
    {
        PickedLocations = new GameObject[2] { PickedLocation1, PickedLocation2 };
    }

    // Update is called once per frame
    void Update()
    {
        
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
            } else
            {
                same = false;
            }
        }else
        {
            if (_pickedCards.Any())
            {
                same= false;
            }
        }

        if (!same){
            Debug.Log("dif");
                foreach(Transform child in pickCardsHolder.transform) {
                    Destroy(child.gameObject);
                }
            for (int i = 0; i<_pickedCards.Count; i++) {
                GameObject Temp = Instantiate(_pickedCards[i], PickedLocations[i].transform.position, _pickedCards[i].transform.rotation);
                Temp.SetActive(true);
                Temp.transform.parent = pickCardsHolder.transform;
                foreach(Transform child in Temp.transform)
                {

                child.tag = "Picked";
                }
            }
            pickedCards =   new List<GameObject>(_pickedCards);
            same = true;
        }
        
    }
}
