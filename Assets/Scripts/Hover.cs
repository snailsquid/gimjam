using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public string selectableTag = "Selectable";
    private Transform _selection;
    public List<GameObject> pickedCards = new();

    public Animator[] selectAnimator;
    public Animator[] unselectAnimator;

    public GameObject GameScript;

    private void OnMouseExit()
    {
        if (gameObject.transform.CompareTag(selectableTag))
        {
            unselectAnimator = gameObject.transform.parent.GetComponentsInChildren<Animator>();
            foreach (Animator animator in unselectAnimator)
            {
                Debug.Log("aaaa");
                animator.Play("UnselectCard", 0, 0.0f);
            }
        }
    }

    void OnMouseDown()
    {
        var selection = gameObject.transform;
        if (selection.CompareTag(selectableTag))
        {

        foreach (Transform child in selection.parent)
        {
            child.GetComponent<Animator>().Play("ClickPick", 0, 0.0f);
            child.localPosition = new Vector3(0, 0, 0);
        }
        if (GameScript.GetComponent<Game>()._pickedCards.Count >= 2)
        {
            Debug.Log("bigg");
            GameScript.GetComponent<Game>()._pickedCards[0].SetActive(true);
            GameScript.GetComponent<Game>()._pickedCards.RemoveAt(0);
        }

        GameScript.GetComponent<Game>()._pickedCards.Add(selection.parent.gameObject);

        selection.parent.gameObject.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        if (gameObject.transform.CompareTag(selectableTag))
        {

        GameObject hit = gameObject;
            var selection = hit.transform;
            selection.GetComponent<Animator>().Play("SelectCard", 0, 0.0f);
                _selection = selection;
            

        }
        }
    }

