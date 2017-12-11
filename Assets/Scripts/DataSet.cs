using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSet : MonoBehaviour
{

    public string csvFilePath;
    public List<string> dataSetLabels = new List<string>();
    public List<DataPoint> listDataPoints = new List<DataPoint>();

    // Use this for initialization
    void Awake()
    {
        ReadDataset();
    }

    //Function ReadDataset :
    //read the csv-file at the csvFilePath directory
    //the first line will get saved in the list dataSetLabels
    //each other line creats a DataPoint instance and add it to listDataPoints
    void ReadDataset()
    {
        bool firstline = true;
        StreamReader sr = new StreamReader(csvFilePath);
        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split(',');

            if (firstline)
            {
                foreach (string element in line)
                {
                    dataSetLabels.Add(element);
                }
                firstline = false;
            }
            else
            {
                List<string> values = new List<string>();

                foreach (string element in line)
                {
                    values.Add(element);
                }
                if (dataSetLabels.Count == values.Count)
                {
                    DataPoint point = new DataPoint(values, dataSetLabels);
                    listDataPoints.Add(point);
                }
            }

        }
    }

}

// class DataPoint:
// each datapoint has two list to store several values and there labels
public class DataPoint
{

    public List<string> valueList;
    public List<string> labelList;

    //DataPoint Constructor
    public DataPoint(List<string> values, List<string> labels)
    {
        valueList = values;
        labelList = labels;
    }

    //funtion GetValue:
    //returns the assigned value for a given label 
    public string GetValue(string label)
    {
        if (labelList.Contains(label))
        {
            int index = labelList.FindIndex(a => a == label);
            return valueList[index];
        }
        else
        {
            return "empty axis";
        }
    }
}