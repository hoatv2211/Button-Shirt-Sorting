
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UIButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    private Transform parent;
    private Vector3 localScaleOld;

    [SerializeField] private float scaleRate = 0.95f;
    [SerializeField] private bool isFxz = true; //fxz

    public bool interactable = true;
    private void Awake()
    {
        parent = this.transform.parent;
        localScaleOld = this.transform.localScale;
    }

    public UnityEvent ClickEvent = new UnityEvent();

    public void OnPointerUp(PointerEventData data)
    {
        if (isFxz && this.interactable)
            transform.localScale = localScaleOld;
    }

    public void OnPointerDown(PointerEventData data)
    {
        SoundManager.Instance.PlayFx();
        
        if (isFxz&& this.interactable)
            transform.localScale = localScaleOld * scaleRate;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!this.interactable) return;
        //Module.LowVibrate();
        ClickEvent.Invoke();
    }

    public void SetUpEvent(UnityAction action)
    {
        ClickEvent.RemoveAllListeners();
        if(action != null)
        ClickEvent.AddListener(action);
    }
}
