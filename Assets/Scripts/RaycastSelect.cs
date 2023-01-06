using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RaycastSelect : MonoBehaviour
{
    public string selectableTag = "Selectable";
    public Material highlightMaterial;
    public Material defaultMaterial;
    public int selectOffset;

    public float speed = 0.5f;

    public Animator[] selectAnimator;
    public Animator[] unselectAnimator;

    private Transform _selection;

    void resetCard()
    {
        if(_selection!= null&&_selection.parent!=null) {
             var selectionRenderer = _selection.GetComponent<Renderer>();
            unselectAnimator = _selection.parent.GetComponentsInChildren<Animator>();
            foreach(Animator animator in unselectAnimator)
            {
                animator.Play("UnselectCard", 0, 0.0f);
            }
            selectionRenderer.material = defaultMaterial;
            Debug.Log(_selection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer!= null)
                {
                    if (selection == _selection)
                    {

                    if (selection.CompareTag(selectableTag))
                    {
                        selectionRenderer.material = highlightMaterial;
                    }
                    } else {
                    if (_selection != null&&selection.parent!=null&&_selection.GetComponent<Renderer>()!=null)
                    {
                        if (selectionRenderer.material != _selection.GetComponent<Renderer>().material)
                        {
                            Debug.Log("first highlight");
                            //selection.parent.position = new Vector3(selection.parent.position.x, -5, selection.parent.position.z);
                            selectAnimator = selection.parent.GetComponentsInChildren<Animator>();
                            foreach (Animator animator in selectAnimator)
                            {
                                animator.Play("SelectCard", 0, 0.0f);
                            }
                        }
                    }
                    Debug.Log("no highlight");
                        resetCard();
                    }
                } 
                    _selection = selection;
            }
        
    }
}
