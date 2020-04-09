using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;
using BarGraph.VittorCloud;

public class BarGraphExample : MonoBehaviour
{
    public List<BarGraphDataSet> exampleDataSet; // public data set for inserting data into the bar graph
    BarGraphGenerator barGraphGenerator;

    //Desde el inspector se puede poner a true o false
    //permitiendo así mostrar el código del ejemplo de la librearía
    // o los datos metidos a mano.
    public bool useCode = false;
    void Start()
    {
        barGraphGenerator = GetComponent<BarGraphGenerator>();


        //if the exampleDataSet list is empty then return.
        if (exampleDataSet.Count == 0)
        {

            Debug.LogError("ExampleDataSet is Empty!");
            return;
        }

        if (useCode)
        {
            //Borra los datos introducidos desde el inspector
            exampleDataSet.Clear();

            //Creo un nuevo conjunto de datos
            BarGraphDataSet example = new BarGraphDataSet();
            //Asigno el color y el nombre
            example.barColor = Color.blue;
            example.GroupName = "Grupo de prueba Casos Activos";
            //Voy a configurar varios paises
            example.ListOfBars = new List<XYBarValues>();
            //Primer país
            XYBarValues xySpain = new XYBarValues();
            xySpain.XValue = "España";
            xySpain.YValue = 100.0f;  //Cantidad de contagiados
                                      //Añade el país al conjunto de datos
            example.ListOfBars.Add(xySpain);

            //Otro país repite los pasos con otros datos.
            XYBarValues xyItaly = new XYBarValues();
            xyItaly.XValue = "Italy";
            xyItaly.YValue = 250.0f;  //Cantidad de contagiados
            example.ListOfBars.Add(xyItaly);

            //Añade el conjunto de datos a mostrar
            exampleDataSet.Add(example);


            example = new BarGraphDataSet();
            //Asigno el color y el nombre
            example.barColor = Color.green;
            example.GroupName = "Grupo de prueba Recuperados";
            //Voy a configurar varios paises
            example.ListOfBars = new List<XYBarValues>();
            //Primer país
             xySpain = new XYBarValues();
            xySpain.XValue = "España";
            xySpain.YValue = 1200.0f;  //Cantidad de contagiados
                                      //Añade el país al conjunto de datos
            example.ListOfBars.Add(xySpain);

            //Otro país repite los pasos con otros datos.
            xyItaly = new XYBarValues();
            xyItaly.XValue = "Italy";
            xyItaly.YValue = 2500.0f;  //Cantidad de contagiados
            example.ListOfBars.Add(xyItaly);
            exampleDataSet.Add(example);

        }

        //Generar la gráfica
        barGraphGenerator.GeneratBarGraph(exampleDataSet);

    }


    //call when the graph starting animation completed,  for updating the data on run time
    public void StartUpdatingGraph()
    {
        //Comento la siguiente linea para evitar que se generen gráficas con datos aleatorios
        //tras acabar de animar la gráfica.
       
       // StartCoroutine(CreateDataSet());
    }

  
    //Funcion que genera datos aleatorios cada tres segundos...
    IEnumerator CreateDataSet()
    {
        //  yield return new WaitForSeconds(3.0f);
        while (true)
        {

            GenerateRandomData();

            yield return new WaitForSeconds(2.0f);

        }

    }



    //Generates the random data for the created bars
    void GenerateRandomData()
    {
        
        int dataSetIndex = UnityEngine.Random.Range(0, exampleDataSet.Count);
        int xyValueIndex = UnityEngine.Random.Range(0, exampleDataSet[dataSetIndex].ListOfBars.Count);
        exampleDataSet[dataSetIndex].ListOfBars[xyValueIndex].YValue = UnityEngine.Random.Range(barGraphGenerator.yMinValue, barGraphGenerator.yMaxValue);
        barGraphGenerator.AddNewDataSet(dataSetIndex, xyValueIndex, exampleDataSet[dataSetIndex].ListOfBars[xyValueIndex].YValue);
    }
}



