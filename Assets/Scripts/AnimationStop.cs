using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStop : MonoBehaviour
{
    public GameObject game, database;
    void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = database.GetComponent<CardDatabase>().cardImages[gameObject.GetComponent<CardDisplay>().id];
    }
}
