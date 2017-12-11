using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction_DataMenu : MonoBehaviour
{

    public DataSet dataset;

    public Dropdown xDropdown;
    public Dropdown yDropdown;
    public Dropdown zDropdown;


    // fill the three given dropdowns with the dataSetLabels list
    void Start()
    {
        xDropdown.ClearOptions();
        yDropdown.ClearOptions();
        zDropdown.ClearOptions();

        xDropdown.AddOptions(dataset.dataSetLabels);
        yDropdown.AddOptions(dataset.dataSetLabels);
        zDropdown.AddOptions(dataset.dataSetLabels);
    }


    //when a new item is chosen in a dropbox, the Dropbox_IndexChange function is called
    //the index refers the index of the chosen option
    public void xDropdown_IndexChange(int index)
    {
        gameObject.GetComponent<PlotData>().xAxis = dataset.dataSetLabels[index];
    }

    public void yDropdown_IndexChange(int index)
    {
        gameObject.GetComponent<PlotData>().yAxis = dataset.dataSetLabels[index];
    }

    public void zDropdown_IndexChange(int index)
    {
        gameObject.GetComponent<PlotData>().zAxis = dataset.dataSetLabels[index];
    }

    //the ButtonPress function is called when the button is pressed
    public void ButtonPress()
    {
        gameObject.GetComponent<PlotData>().newplot = true;
    }


}

