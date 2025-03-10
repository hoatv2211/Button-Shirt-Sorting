
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ButtonCtrl : MonoBehaviour
{
    public string id;
    public GameObject objPlaced;
    private Vector3 originalPosition;
    private GameObject vfxHint;

    private bool isPlaced = false;
    public bool IsPlaced => isPlaced;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void SetColor(Color color)
    {
        id = color.ToString();
        GetComponent<SpriteRenderer>().color = color;
        objPlaced.SetActive(false);
    }

    private void OnMouseDrag()
    {
        if (GameplayCtrl.Instance == null) //Tool scenes disable
            return;

        if (!isPlaced)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.Lerp(transform.position,mousePosition + Vector3.up,1);
        }
    }

    private void OnMouseUp()
    {
        if (GameplayCtrl.Instance == null) //Tool scenes disable
            return;

        if (isPlaced) return;

        LayerMask slotLayer = LayerMask.GetMask("Slot");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.25f, slotLayer);
        List<ShirtSlot> matchingSlots = new List<ShirtSlot>();
        foreach (Collider2D hitCollider in hitColliders)
        {
            ShirtSlot slot = hitCollider.GetComponent<ShirtSlot>();
            if (slot != null && slot.IsMatchingColor(id))
            {
                matchingSlots.Add(slot);
            }
        }

        if (matchingSlots.Count > 0)
        {
            // pick Slot 
            ShirtSlot bestSlot = matchingSlots.OrderBy(slot => Vector2.Distance(transform.position, slot.transform.position)).First();

            transform.position = bestSlot.transform.position;
            isPlaced = true;
            objPlaced.SetActive(true);

            GameplayCtrl.Instance.RemainChecking(bestSlot, this);
            return;
        }
 
        transform.position = originalPosition;
    }

    public void SetAuto(ShirtSlot slot)
    {
        transform.position = slot.transform.position;
        isPlaced = true;
        objPlaced.SetActive(true);

        GameplayCtrl.Instance.RemainChecking(slot, this);
    }


    Tween twEffect = null;
    public void ShowHintEffect()
    {
        //show fx
        //twEffect = GetComponent<SpriteRenderer>().DOFade(0.5f, 1).SetLoops(-1, LoopType.Yoyo);
        vfxHint = GameplayCtrl.Instance.EffectHint(transform.position);
        vfxHint.transform.SetParent(transform);
    }

    public void HideHintEffect()
    {
        //if(twEffect!=null)
        //    twEffect.Kill();

        if (vfxHint != null)
            SimplePool.Despawn(vfxHint);
    }
}
