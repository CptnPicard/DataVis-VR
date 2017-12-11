using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class InteractionDatapoint : VRTK_InteractableObject
{

    public DataPoint datapoint;
    private string newInfoText;


    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        newInfoText = "";
        //write a string with all labels and values
        for (var a = 0; a < datapoint.labelList.Count; a++)
        {
            newInfoText += datapoint.labelList[a] + " :\t" + datapoint.valueList[a] + "\n";
        }

        //look for the GameObject with tag "Infotext", change the text from its Text component
        GameObject.FindWithTag("Infotext").GetComponent<Text>().text = newInfoText;
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
    }


}