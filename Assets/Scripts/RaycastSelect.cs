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

    private float duration = 3f;
    private float elapsedTime;

    private Transform _selection;

    void resetCard(float percentageComplete)
    {
        if(_selection!= null) { 
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection.parent.position = Vector3.Lerp(new Vector3(_selection.parent.position.x,-5, _selection.parent.position.z), new Vector3(_selection.parent.position.x, -15, _selection.parent.position.z),duration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;
        resetCard(percentageComplete);

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
                        selection.parent.position = Vector3.Lerp(new Vector3(selection.parent.position.x, -15, selection.parent.position.z), new Vector3(selection.parent.position.x, -5, selection.parent.position.z), duration);
                        selectionRenderer.material = highlightMaterial;
                    }
                    else { 
                        resetCard(percentageComplete);
                    }
                } 
                    _selection = selection;
            }
        }
    }
}
