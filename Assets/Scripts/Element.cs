using System;

[Serializable]
public class Element
{
    public string elementName;
    public int elementId;
    public Element(string elementName, int elementId) { 
        this.elementName = elementName;
        this.elementId = elementId;
    }
}
