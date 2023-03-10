using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string tipToShow;
    private float timeToWait = 0.5f;
    int test = 10;
    //public Item item;
    void Start()
    {
        tipToShow = "" + test;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverTipManager.OnMouseLoseFocus();
    }
    private void ShowMessage()
    {
        HoverTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
