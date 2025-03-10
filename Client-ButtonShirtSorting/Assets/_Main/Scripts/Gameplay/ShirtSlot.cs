using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShirtSlot : MonoBehaviour
{
    public string id;
    private GameObject vfxHint;
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
        vfxHint = GameplayCtrl.Instance.EffectHint(transform.position);
    }

    public void HideHintEffect()
    {

        if (vfxHint != null)
            SimplePool.Despawn(vfxHint);
    }
}
