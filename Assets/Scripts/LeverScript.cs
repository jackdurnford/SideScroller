using UnityEngine;

public class LeverScript : MonoBehaviour
{
    private bool _shootText;
    public GameObject Lever2;
    public GameObject ShootText;
    private GameObject Spear;
    private SpearScript Spearscript;
    private GameObject[] textObject;
    // Use this for initialization

    private void Start()
    {
        Lever2 = GameObject.FindGameObjectWithTag("Lever2");
        Spear = GameObject.FindGameObjectWithTag("Spear");
        Spearscript = Spear.GetComponent<SpearScript>();

        ;
    }

    private void Update()
    {
        if (_shootText)
        {
            Instantiate(ShootText, new Vector3(37.8f, -51.5f, 2.52f), Quaternion.identity);
            _shootText = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Lever")
        {
            _shootText = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Lever")
        {
            if (Input.GetKeyDown("e"))
            {
                Spearscript.Shoot();
            }
        }
    }

    private void OnTriggerExit(Collider coli)
    {
        if (coli.gameObject.tag == "Lever")
        {
            _shootText = false;
            DeleteShootText();
        }
    }

    private void DeleteShootText()
    {
        textObject = GameObject.FindGameObjectsWithTag("ShootText");
        for (var i = 0; i < textObject.Length; i++)
        {
            Destroy(textObject[i]);
        }
    }
}