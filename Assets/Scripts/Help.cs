using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject HelpObject;

    public void open()
    {
        HelpObject.SetActive(true);
    }

    public void close()
    {
        HelpObject.SetActive(false);
    }
}
