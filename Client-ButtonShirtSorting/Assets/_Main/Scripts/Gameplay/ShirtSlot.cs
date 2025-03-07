using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtSlot : MonoBehaviour
{
    private Color slotColor;

    public void SetColor(Color color)
    {
        slotColor = color;
        GetComponent<SpriteRenderer>().color = color;
       
    }

    public bool IsMatchingColor(Color buttonColor)
    {
        return slotColor == buttonColor;
    }
}
