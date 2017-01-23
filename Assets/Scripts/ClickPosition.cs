using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosition : MonoBehaviour {

    SpriteRenderer renderer;

	void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("klik");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                gameObject.transform.position = hit.point;
            }
        }


        if (Time.timeScale == 0.2f)
        {
            Debug.Log("true " + Time.timeScale);
            //gameObject.SetActive(false);
            renderer.enabled = false;
        }
        else
        {
            Debug.Log("false " + Time.timeScale);
            //gameObject.SetActive(true);
            renderer.enabled = true;
        }
    }
}
