using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrawButtonClick : MonoBehaviour
{
    public GameObject gameSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw(int amount)
    {
        gameSystem.GetComponent<Game>().drawCard(amount);
    }
    public void Play()
    {
        gameSystem.GetComponent<Game>().play();
    }
}
