using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountrySelector : MonoBehaviour
{

    public string countryName = "Spain";
    public string countryCode = "spain";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        StartCoroutine(RestManager.instance.GetCountry(countryCode));
    }
}
