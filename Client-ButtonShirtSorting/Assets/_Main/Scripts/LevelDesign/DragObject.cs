using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use In secene tools level
/// </summary>
public class DragObject : MonoBehaviour
{
    private void OnMouseDrag()
    {
        if (GameplayCtrl.Instance == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
       

    }

  
}
