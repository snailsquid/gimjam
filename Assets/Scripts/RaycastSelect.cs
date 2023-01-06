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

    private Transform _selection;

    void resetCard()
    {
        if(_selection!= null) { 
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection.parent.position = new Vector3(_selection.parent.position.x, -15, _selection.parent.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        resetCard();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag)) {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer!= null)
                {
                    if (selection == _selection)
                    {
                        selection.parent.position = new Vector3(selection.parent.position.x, selectOffset, selection.parent.position.z);
                        selectionRenderer.material = highlightMaterial;
                    }
                    else { 
                        resetCard();
                    }
                } 
                    _selection = selection;
            }
        }
    }
}
