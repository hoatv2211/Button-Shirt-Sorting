using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShirtSlot : MonoBehaviour
{
    public string id;

    public void SetColor(Color color)
    {
        id = color.ToString();
        GetComponent<SpriteRenderer>().color = color;
       
    }

    public bool IsMatchingColor(string _id)
    {
        return id == _id;
    }

    public void ShowHintEffect()
    {
        GetComponent<SpriteRenderer>().DOFade(0.5f, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void HideHintEffect()
    {

    }
}
