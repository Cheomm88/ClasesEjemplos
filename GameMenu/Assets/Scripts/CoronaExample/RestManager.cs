using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;


public class RestManager : MonoBehaviour
{
    //Instacia del script para poder acceder desde otros lados por código
    public static RestManager instance;

    //Pantalla de carga y texto con la información de los casos
    public GameObject loadingScreen;
    public Text infoText; 


    void Awake()
    {
        //Creamos un singleton (solo puede existir un RestManager)
        if (RestManager.instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); 
        }
    }


    void Start()
    {
        //Carga el resumen de la pantalla inicial
        //Al ser una función que tardará en base a la conexión y respuesta del servidor
        //es una rutina de trabajo por lo que es necesario ponerle StarCoroutine.
        StartCoroutine(GetAll());
        //Carga todos los paises y calcula el máximo.
        StartCoroutine(GetCountries());
    }

    //Su tiempo de ejecución depende de la descarga por eso devuelve IEnumerator
    public IEnumerator GetAll()
    {
        //Activa la pantalla de carga
        loadingScreen.SetActive(true);
        infoText.text = "Cargando ...";

        //Hace petición de descarga
        UnityWebRequest www = UnityWebRequest.Get("https://corona.lmao.ninja/all");
        yield return www.SendWebRequest(); 

        //Desactiva la pantalla de carga
        loadingScreen.SetActive(false);

        //En caso de error
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            infoText.text = "Error : " + www.error;
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            //Convierte a JSONNode la información descargada
            JSONNode data = JSON.Parse(www.downloadHandler.text);

            //Muestra los datos
            infoText.text = "Casos " + data["cases"] + "\n";
            infoText.text += "Muertes " + data["deaths"] + "\n";
            infoText.text += "Recuperados " + data["recovered"] + "\n";

        }
    }

    public IEnumerator GetCountry(string country = "spain")
    {
        loadingScreen.SetActive(true);
        infoText.text = "Cargando ...";

        //La URL de la petición depende del parámetro country, por defecto es españa pero desde country selector es modificado para cada país seleccionable.
        UnityWebRequest www = UnityWebRequest.Get("https://corona.lmao.ninja/countries/" + country);
        yield return www.SendWebRequest();


        loadingScreen.SetActive(false);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            JSONNode data = JSON.Parse(www.downloadHandler.text);

            infoText.text = "País " + data["country"] + "\n";
            infoText.text += "Casos " + data["cases"] + "\n";
            infoText.text += "Casos hoy " + data["todayCases"] + "\n";
            infoText.text += "Muertes " + data["deaths"] + "\n";
            infoText.text += "Muertes hoy " + data["todayDeaths"] + "\n";
            infoText.text += "Recuperados " + data["recovered"] + "\n";
            infoText.text += "Críticos " + data["critical"] + "\n";
        }
    }

    public IEnumerator GetCountries()
    {
        loadingScreen.SetActive(true);
        infoText.text = "Cargando ...";

        UnityWebRequest www = UnityWebRequest.Get("https://corona.lmao.ninja/countries");
        yield return www.SendWebRequest();

        loadingScreen.SetActive(false);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            //Va a calcular el país con más muertes.
            JSONNode data = JSON.Parse(www.downloadHandler.text);
     
           //El primer país será mi máximo de manera inicial
            JSONNode maxDeaths = data[0];
            //Recorro uno a uno cada país
            foreach (JSONNode infoCountry in data)
            {
                Debug.Log(infoCountry["country"]);
                Debug.Log(infoCountry["cases"]);

                //Comparo el número de muertes y si el país que estoy mirando ahora tiene más muertes
                //actualizo mi máximo.
                if (maxDeaths["deaths"] < infoCountry["deaths"])
                {
                    maxDeaths = infoCountry;
                }
            }

            Debug.Log("Pais con más muertes" + maxDeaths["country"] + " con " + maxDeaths["deaths"]);
        }
    }

}
