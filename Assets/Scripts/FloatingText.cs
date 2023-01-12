using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public GameObject healthModText, pLoc, eLoc;
    public void PlayerHealth(Color color, string text)
    {
        var floating = Instantiate(healthModText, pLoc.transform);
        floating.transform.GetChild(0).GetComponent<TextMesh>().text = text;
        floating.transform.GetChild(0).GetComponent<TextMesh>().color = color;
    }
    public void EnemyHealth(Color color, string text)
    {
        var floating = Instantiate(healthModText, eLoc.transform);
        floating.transform.GetChild(0).GetComponent<TextMesh>().text = text;
        floating.transform.GetChild(0).GetComponent<TextMesh>().color = color;
    }

    public void Combo(string text)
    {
        var combo = Instantiate(healthModText, eLoc.transform);
        combo.transform.GetChild(0).GetComponent<TextMesh>().text = text;
    }
}
