using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void destroyOnFinish()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
