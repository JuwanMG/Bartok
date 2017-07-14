using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotDef
{
    public float x;
    public float y;
    public bool faceUp = false;
    public string layerName = "Default";
    public int layerID = 0;
    public int id;
    public List<int> hiddenBy = new List<int>();
    public float rot;
    public string type = "slot";
    public Vector2 stagger;
    public int player;
    public Vector3 pos;
}

public class BartokLayout : MonoBehaviour
{
    public PT_XMLReader xmlr; 
public PT_XMLHashtable xml; 
public Vector2 multiplier; 
    public List<SlotDef> slotDefs; // The SlotDefs hands
    public SlotDef drawPile;
    public SlotDef discardPile;
    public SlotDef target;
    // This function is called to read in the LayoutXML.xml file
    public void ReadLayout(string xmlText)
    {
        xmlr = new PT_XMLReader();
        xmlr.Parse(xmlText); // The XML is parsed
        xml = xmlr.xml["xml"][0]; 
        multiplier.x = float.Parse(xml["multiplier"][0].att("x"));
        multiplier.y = float.Parse(xml["multiplier"][0].att("y"));
        // Read in the slots
        SlotDef tSD;
        // slotsX is used as a shortcut to all the <slot>s
        PT_XMLHashList slotsX = xml["slot"];
        for (int i = 0; i < slotsX.Count; i++)
        {
            tSD = new SlotDef(); // Create a new SlotDef instance
            if (slotsX[i].HasAtt("type"))
            {
                // If this <slot> has a type attribute parse it
                tSD.type = slotsX[i].att("type");
            }
            else
            {
                // If not, set its type to “slot”
                tSD.type = "slot";
            }
            // Various attributes are parsed into numerical values
            tSD.x = float.Parse(slotsX[i].att("x"));
            tSD.y = float.Parse(slotsX[i].att("y"));
            tSD.pos = new Vector3(tSD.x * multiplier.x,
            tSD.y * multiplier.y, 0);
            // Sorting Layers
            tSD.layerID = int.Parse(slotsX[i].att("layer"));
           
        // This converts the number of the layerID into a text layerName
tSD.layerName = tSD.layerID.ToString();
            // The layers are used to make sure that the correct cards are
            // on top of the others. In Unity 2D, all of our assets are
            // effectively at the same Z depth, so sorting layers are used
            // to differentiate between them.
            // pull additional attributes based on the type of each <slot>
            switch (tSD.type)
            {
                case "slot":
// ignore slots that are just of the “slot” type
break;
                case "drawpile":

            tSD.stagger.x = float.Parse(slotsX[i].att("xstagger"));
                    drawPile = tSD;
                    break;
                case "discardpile":
discardPile = tSD;
                    break;
                case "target":
// The target card has a different layer from discardPile
target = tSD;
                    break;
                case "hand":
// Information for each player’s hand
tSD.player = int.Parse(slotsX[i].att("player"));
                    tSD.rot = float.Parse(slotsX[i].att("rot"));
                    slotDefs.Add(tSD);
                    break;
            }
        }
    }
}

    
