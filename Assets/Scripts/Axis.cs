using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Axis : MonoBehaviour
{

    public DataSet dataSet;

    public Dictionary<string, float> xDict;
    public Dictionary<string, float> yDict;
    public Dictionary<string, float> zDict;


    //function SpawnFullCoordinateSystem:
    //calls function SpawnAxis to create axis in all three planes
    public void SpawnCoSystem(int axisPerPlane, string xlabel, string ylabel, string zlabel)
    {
        xDict = ScaleAxis(xlabel);
        yDict = ScaleAxis(ylabel);
        zDict = ScaleAxis(zlabel);

        int skipX = Convert.ToInt32(xDict.Count / axisPerPlane);
        int skipY = Convert.ToInt32(yDict.Count / axisPerPlane);
        int skipZ = Convert.ToInt32(zDict.Count / axisPerPlane);

        int counter = 0;
        foreach (string key in xDict.Keys)
        {
            if (counter == skipX)
            {
                SpawnAxis("X", "XZ-Plane", xDict[key], key);
                counter = 0;
            }
            else
            {
                counter += 1;
            }
        }

        counter = 0;
        foreach (string key in yDict.Keys)
        {
            if (counter == skipY)
            {
                SpawnAxis("Y", "XY-Plane", yDict[key], key);
                SpawnAxis("Y", "YZ-Plane", yDict[key], key);
                counter = 0;
            }
            else
            {
                counter += 1;
            }
        }

        counter = 0;
        foreach (string key in zDict.Keys)
        {
            if (counter == skipZ)
            {
                SpawnAxis("Z", "XZ-Plane", zDict[key], key);
                counter = 0;
            }
            else
            {
                counter += 1;
            }
        }


    }

    //function SpawnAxis
    //create one GameObject that represents an axis and one GameObject by calling AddTextMesh
    //the orientation is given through the string coordinate, the position through the
    //string plane and the float position. 
    public void SpawnAxis(string coordinate, string plane, float position, string label)
    {
        //create a new GameObject axis, name and tag it
        GameObject Axis = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Axis.transform.name = "Axis";
        Axis.tag = "Axis";

        //look for a GameObject named like string plane, set it as parent
        Axis.transform.parent = GameObject.Find(plane).transform;

        //color axis
        Renderer rend = Axis.GetComponent<Renderer>();
        rend.material.color = new Color(1, 0, 0, 0);

        //set axis Transform and call AddTextMesh depeding on plane and coordinate
        Vector3 scalePlane = GameObject.Find(plane).transform.localScale;
        if (coordinate == "Z")
        {
            Axis.transform.localScale = new Vector3(scalePlane.x, 0.01f, 0.01f);
            AddTextMesh(label, Axis, 1, 0, TextAnchor.UpperLeft);
        }
        if (coordinate == "Y")
        {
            if (plane == "XY-Plane")
            {
                Axis.transform.localScale = new Vector3(scalePlane.x, 0.01f, 0.01f);
                //AddTextMesh(label, Axis,-1, 180, TextAnchor.MiddleRight);
            }
            if (plane == "YZ-Plane")
            {
                Axis.transform.localScale = new Vector3(0.01f, 0.01f, scalePlane.z);
                AddTextMesh(label, Axis, -1, -90, TextAnchor.MiddleRight);
            }
        }
        if (coordinate == "X")
        {
            Axis.transform.localScale = new Vector3(0.01f, 0.01f, scalePlane.z);
            AddTextMesh(label, Axis, -1, -90, TextAnchor.UpperRight);
        }

        if (plane == "XY-Plane")
        {
            Axis.transform.localPosition = new Vector3(0.0f, position - 0.5f * scalePlane.y, 0.0f);
        }
        if (plane == "XZ-Plane")
        {
            if (coordinate == "Z")
            {
                Axis.transform.localPosition = new Vector3(0.0f, 0.0f, position - 0.5f * scalePlane.z);
            }
            if (coordinate == "X")
            {
                Axis.transform.localPosition = new Vector3(position - 0.5f * scalePlane.x, 0.0f, 0.0f);
            }
        }
        if (plane == "YZ-Plane")
        {
            Axis.transform.localPosition = new Vector3(0.0f, position - 0.5f * scalePlane.z, 0.0f);
        }



    }

    //funtion ScaleAxis:
    //the function takes a list of labels and length for a axis
    //a dictionary links each label with a specific value between 0 and 1
    //the function returns a dictionary that contains the links
    public Dictionary<string, float> ScaleAxis(string label)
    {
        Dictionary<string, float> scale = new Dictionary<string, float>();
        List<string> allValues = new List<string>();
        float ticDistance;
        int counter = 1;

        //add different values once to the list allValues
        foreach (DataPoint point in dataSet.listDataPoints)
        {
            if (!allValues.Contains(point.GetValue(label)))
            {
                allValues.Add(point.GetValue(label));
            }

        }

        //sorting all values (if possible) by calling SortStringList
        //mapping a value between 0 and 1 to each value in the list allValues
        allValues = SortStringList(allValues);
        ticDistance = 1.0f / (allValues.Count + 1);
        foreach (string value in allValues)
        {
            scale.Add(value, counter * ticDistance);
            counter += 1;
        }
        return scale;
    }

    //function AddTextMesh:
    // takes a string, a parent GameObject and a position to create a textmesh
    void AddTextMesh(string text, GameObject parent, float distance, float rotationY, TextAnchor anchor)
    {
        // creating an empty GameObject, adding textMesh component and MeshRenderer
        var label = new GameObject();
        label.name = "label";
        var textMesh = label.AddComponent<TextMesh>();

        // defining the position and orientation relative to the given parent
        textMesh.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        textMesh.anchor = anchor;
        textMesh.transform.parent = parent.transform;
        textMesh.transform.localPosition = parent.transform.localScale * 0.5f * distance;

        // set the font size
        textMesh.fontSize = 40;
        textMesh.characterSize = 0.01f;

        // set the text color
        textMesh.color = Color.white;

        // set the text
        textMesh.text = text;
    }

    //function SortStringList:
    //convert a string list into a float list, sort it, convert it back
    private List<string> SortStringList(List<string> stringlist)
    {
        try
        {
            List<float> intList = stringlist.ConvertAll(float.Parse);
            intList.Sort();
            stringlist = intList.ConvertAll<string>(delegate(float i) {return i.ToString(); });
            return stringlist;
        }
        catch
        {
            stringlist.Sort();
            return stringlist;

        }
    }
}

