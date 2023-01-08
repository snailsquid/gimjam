using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RaycastSelect : MonoBehaviour
{
    public string selectableTag = "Selectable";
    public Material highlightMaterial;
    public Material defaultMaterial;
    public int selectOffset;

    //pick
    public List<GameObject> pickedCards = new();

    public GameObject system;


    public float speed = 0.5f;

    public Animator[] selectAnimator;
    public Animator[] unselectAnimator;

    private Transform _selection;

    void resetCard()
    {
        if (_selection != null && _selection.parent != null && _selection.GetComponent<Renderer>() != null&& _selection.CompareTag(selectableTag))
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            unselectAnimator = _selection.parent.GetComponentsInChildren<Animator>();
            foreach (Animator animator in unselectAnimator)
            {
                animator.Play("UnselectCard", 0, 0.0f);
            }
            selectionRenderer.material = defaultMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                if (selection == _selection)
                {

                    if (selection.CompareTag(selectableTag))
                    {
                        selectionRenderer.material = highlightMaterial;

                        if (Input.GetMouseButtonDown(0))
                        {
                            selectionRenderer.material = defaultMaterial;
                            foreach (Transform child in selection.parent)
                            {
                                child.GetComponent<Animator>().Play("UnselectCard", 0, 0.0f);
                                Debug.Log(child.localPosition);
                                child.localPosition = new Vector3(0, 0, 0);
                            }
                            if (pickedCards.Count >= 2)
                            {
                                pickedCards[0].SetActive(true);
                                pickedCards.RemoveAt(0);
                            }
                            pickedCards.Add(selection.parent.gameObject);

                            selection.parent.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    if (_selection != null && selection.parent != null && _selection.GetComponent<Renderer>() != null&& selectionRenderer.material != _selection.GetComponent<Renderer>().material&& selection.CompareTag(selectableTag))
                    {
                            selectAnimator = selection.parent.GetComponentsInChildren<Animator>();
                            foreach (Animator animator in selectAnimator)
                            {
                                animator.Play("SelectCard", 0, 0.0f);
                            }
                    }
                    resetCard();
                }
            }
            _selection = selection;
        }

    }
}
