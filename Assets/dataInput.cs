using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

public class dataInput : MonoBehaviour
{

    public chicagoData[] myChicagoData;
    public dataVisualization visualization;

    public List<chicagoData> giftList = new List<chicagoData>();



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetChicagoData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Let's grab that data
    IEnumerator GetChicagoData()
    {
        //add that URL from the City of Chicago Data Portal
        string getDataURL = "https://data.cityofchicago.org/resource/5d79-9xqr.json";
        using (UnityWebRequest www = UnityWebRequest.Get(getDataURL))
        {
            www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {

                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);

                    // Now put the JSON in an array
                    myChicagoData = JsonHelper.getJsonArray<chicagoData>(jsonResult);
                    visualization.visualizeIt();

                }

            }


        }


    }




   

}
