using UnityEngine;
using System.Collections;

public class MenuText : MonoBehaviour {
    TextMesh _txt;
    private int _fontsizze;
    private bool _canpress = false;
	// Use this for initialization
	void Start ()
	{
	    _fontsizze = GetComponent<TextMesh>().fontSize;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            Application.LoadLevel("Scene");
	}

    void OnMouseOver()
    {
        if (_fontsizze < 116)
        {
            GetComponent<TextMesh>().fontSize = 123;
        }
        else
        {
            GetComponent<TextMesh>().fontSize = 147;
        }
    }

    void OnMouseButtonDown()
    {
        if (_canpress)
        {
            Application.LoadLevel("Loading");
        }
    }
    void OnMouseExit()
    {
        GetComponent<TextMesh>().fontSize = _fontsizze;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Start")
        {
            _canpress = true;
        }
    }

    void OnTriggerExit(Collider cola)
    {
        if (cola.gameObject.tag == "Start")
        {
            _canpress = false;
        }
    }
}
