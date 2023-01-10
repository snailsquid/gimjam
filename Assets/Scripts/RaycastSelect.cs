using System.Collections.Generic;
using UnityEngine;

public class RaycastSelect : MonoBehaviour
{
//    public string selectableTag = "Selectable";
//    public Material highlightMaterial;
//    public Material defaultMaterial;
//    public int selectOffset;

//    //pick
//    public List<GameObject> pickedCards = new();

//    public GameObject system;


//    public float speed = 0.5f;

//    public Animator[] selectAnimator;
//    public Animator[] unselectAnimator;

//    private Transform _selection;

//    void resetCard()
//    {
//        if (_selection != null && _selection.parent != null && _selection.GetComponent<Renderer>() != null && _selection.CompareTag(selectableTag))
//        {
//            var selectionRenderer = _selection.GetComponent<Renderer>();
//            unselectAnimator = _selection.parent.GetComponentsInChildren<Animator>();
//            foreach (Animator animator in unselectAnimator)
//            {
//                Debug.Log("aaaa");
//                animator.Play("UnselectCard", 0, 0.0f);
//            }
//            // selectionRenderer.material = defaultMaterial;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up));
//        if (hit.collider != null)
//        {
//            Debug.Log("HIT!");
//            var selection = hit.transform;
//            var selectionRenderer = selection.GetComponent<Renderer>();
//            if (selectionRenderer != null)
//            {
//                if (selection == _selection)
//                {

//                    if (selection.CompareTag(selectableTag))
//                    {
//                        // selectionRenderer.material = highlightMaterial;

//                        if (Input.GetMouseButtonDown(0))
//                        {
//                            // selectionRenderer.material = defaultMaterial;
//                            foreach (Transform child in selection.parent)
//                            {
//                                Debug.Log("bbb");
//                                child.GetComponent<Animator>().Play("ClickPick", 0, 0.0f);
//                                Debug.Log("before " + child.localPosition);
//                                child.localPosition = new Vector3(0, 0, 0);
//                                Debug.Log("after " + child.localPosition);
//                            }
//                            if (pickedCards.Count >= 2)
//                            {
//                                pickedCards[0].SetActive(true);
//                                pickedCards.RemoveAt(0);
//                            }
//                            pickedCards.Add(selection.parent.gameObject);

//                            selection.parent.gameObject.SetActive(false);
//                        }
//                    }
//                }
//                else
//                {
//                    Debug.Log("yas");
//                    Debug.Log(selection);
//                    if (_selection != null && selection.parent != null && selection != _selection && selection.CompareTag(selectableTag))
//                    {
//                        selection.GetComponent<Animator>().Play("SelectCard", 0, 0.0f);
//                        resetCard();
//                    }
//                }
//                _selection = selection;
//            }

//        }
//    }
}
