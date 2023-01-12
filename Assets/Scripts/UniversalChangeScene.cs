using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UniversalChangeScene : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(changeScene);
    }


    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
