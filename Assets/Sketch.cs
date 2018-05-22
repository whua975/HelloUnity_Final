using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using
using System.Collections;
using UnityEngine.Networking;

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
    string _WebsiteURL = "https://tominfo.azurewebsites.net/tables/Assignment3?zumo-api-version=2.0.0";

    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }

    //string jsonResponse;



    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        //old code string jsonResponse = Request.GET(_WebsiteURL);



        WWW myWww = new WWW(_WebsiteURL);
        while (myWww.isDone == false) ;
        //{ }
        string jsonResponse = myWww.text;

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Boat[] Assignment3= JsonReader.Deserialize<Boat[]>(jsonResponse);

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0;
        int totalCubes = 30;
        float totalDistance = 2.9f;
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Boat Boat in Assignment3)
        {
            //Example of how to use the object
            Debug.Log("This Boat name is: " + Boat.BoatName);
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE

            x = Boat.X;
            y = Boat.Y;
            z = Boat.Z;
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            //float x = 1.8f + sin * totalDistance;
            //float y = 5.0f;
            //float z = 0.0f;

            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);

            newCube.GetComponent<CubeScript>().SetSize(.45f * (1.0f - perc));
            newCube.GetComponent<CubeScript>().rotateSpeed = 0; //.2f + perc * 4.0f;
            newCube.transform.Find("New Text").GetComponent<TextMesh>().text = Boat.BoatName;//"Hullo Again";
            i++;

            //----------------------
        }
	}



    // Update is called once per frame
    void Update () {
	
	}

}
