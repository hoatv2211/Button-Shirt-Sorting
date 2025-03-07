using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    [SerializeField] private GameObject objPlaced;
    private Color buttonColor;
    private Vector3 originalPosition;
    private bool isPlaced = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void SetColor(Color color)
    {
        buttonColor = color;
        GetComponent<SpriteRenderer>().color = color;
        objPlaced.SetActive(false);
    }

    private void OnMouseDrag()
    {
        if (!isPlaced)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.Lerp(transform.position,mousePosition + Vector3.up,0.2f);
        }
    }

    private void OnMouseUp()
    {
        if (isPlaced) return;

        LayerMask slotLayer = LayerMask.GetMask("Slot");
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 0.25f, slotLayer);
        if (hitCollider != null)
        {
            ShirtSlot slot = hitCollider.GetComponent<ShirtSlot>();
            if (slot != null && slot.IsMatchingColor(buttonColor))
            {
                transform.position = slot.transform.position;
                isPlaced = true;
                objPlaced.SetActive(true);

                GameplayCtrl.Instance.RemainChecking(1);
                return;
            }
        }
        transform.position = originalPosition;
    }

 

}
