using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackCircleScript : MonoBehaviour
{
    private Canvas _canvas;
    public Image blackscreen;
    //public GameObject BlackScreenScript;
    private float restartMenuTimer = 0.0f;
    //public GameObject gameobj;

    private void Awake() {
        _canvas = GetComponent<Canvas>();
       //blackscreen = GetComponentInChildren<Image>();
    }

    private void Start() {
        //gameobj.SetActive(false);
        DrawBlackScreen();
        //BlackScreenScript.SetActive(false);
        //OpenBlackScreen();

    }

    // private void Update() {
      

    // }

    public void OpenBlackScreen() {
        StartCoroutine(Transition(.8f,1,0.1f));
        //Invoke("CloseBlackScreen", 2f);
    }

    public void CloseBlackScreen(){
        StartCoroutine(Transition(4,0.1f,0));
    }      

    private void DrawBlackScreen() {
        Rect canvasRect = _canvas.GetComponent<RectTransform>().rect;
        float canvasWidth = canvasRect.width;
        float canvasHeight = canvasRect.height;

        float squareValue = 0f;
        if(canvasWidth > canvasHeight) {
            squareValue = canvasWidth;
        }else {
            squareValue = canvasHeight;
        }

        blackscreen.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }

    private IEnumerator Transition(float duration, float beginRadius, float endRadius) {
        float time = 0f;
        while(time <= duration) {
            time += Time.deltaTime;
            float t = time/duration;
            float radius = Mathf.Lerp(beginRadius, endRadius,t);
            
            blackscreen.material.SetFloat("_Radius", radius);

            yield return null;
        }
    }
}
