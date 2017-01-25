using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosition : MonoBehaviour
{
    private SpriteRenderer renderer;
    private bool allowNewPosition;


    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {

        //Debug.Log("allowed: " + playerScript.isNewPositionAllowed());
        if (Input.GetMouseButtonDown(0) && allowNewPosition)
        {
            //Debug.Log("klik");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                gameObject.transform.position = hit.point;
            }
        }


        if (Time.timeScale == 0.2f)
        {
            //Debug.Log("true " + Time.timeScale);
            renderer.enabled = false;
            allowNewPosition = true;
        }
        else
        {
            //Debug.Log("false " + Time.timeScale);
            renderer.enabled = true;
            allowNewPosition = false;
        }
    }
}
