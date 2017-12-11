using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotData : MonoBehaviour
{

    public DataSet dataSet;
    public Axis axis;

    public string xAxis;
    public string yAxis;
    public string zAxis;

    public bool newplot = false;

    // called once every Frame
    void Update()
    {
        if (newplot)
        {
            PlotNewData(xAxis, yAxis, zAxis);
            newplot = false;
        }
    }

    //function PlotNewData:
    //delete all Gameobject with the tag Axis and datapoint
    //calls SpawnCoSystem to create a new axis
    //calls SpawnDataPoint for each element in listDataPoints
    void PlotNewData(string xAxis, string yAxis, string zAxis)
    {
        //clear all old axis and data point sphere
        GameObject[] oldAxis = GameObject.FindGameObjectsWithTag("Axis");
        foreach (GameObject obj in oldAxis)
        {
            Destroy(obj);
        }
        GameObject[] oldDatapoints = GameObject.FindGameObjectsWithTag("Datapoint");
        foreach (GameObject obj in oldDatapoints)
        {
            Destroy(obj);
        }


        //call SpawnCOSystem to create Axis and set axis.Dict
        axis.SpawnCoSystem(20, xAxis, yAxis, zAxis);

        foreach (DataPoint dat in dataSet.listDataPoints)
        {
            //look up the postion of each datapoint
            float xpos = axis.xDict[dat.GetValue(xAxis)];
            float ypos = axis.yDict[dat.GetValue(yAxis)];
            float zpos = axis.zDict[dat.GetValue(zAxis)];

            SpawnDataPoint(xpos, ypos, zpos, dat);
        }
    }


    //function SpawnDataPoint:
    //creats a GameObject Sphere as a child form Coordinate System at the given coordinates  
    void SpawnDataPoint(float xpos, float ypos, float zpos, DataPoint datapoint)
    {
        DataPoint dat = datapoint;
        GameObject DataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        DataPoint.tag = "Datapoint";
        DataPoint.name = "Data Point";
        DataPoint.transform.parent = GameObject.Find("Coordinate System").transform;
        DataPoint.transform.localPosition = new Vector3(xpos - 0.5f, ypos - 0.5f, zpos - 0.5f);
        DataPoint.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Renderer rend = DataPoint.GetComponent<Renderer>();
        rend.material.color = new Color(0, 0, 1, 0);
        DataPoint.AddComponent<InteractionDatapoint>();
        DataPoint.GetComponent<InteractionDatapoint>().datapoint = dat;
        DataPoint.GetComponent<InteractionDatapoint>().isUsable = true;
    }



}