using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour {
    public PlayerMovement player;
    public CanvasGroup barCanvas;
    public RectTransform bar;
    public Image bari;

    GameObject cam;
    Camera proj;
    RectTransform screenRect;

    public Color color, depletedColor, warnColor;

    void Start() {
        cam = FindObjectOfType<Camera>().gameObject;
        proj = cam.GetComponent<Camera>();
        Canvas canvas = FindObjectOfType<Canvas>();
        screenRect = canvas.GetComponent<RectTransform>();
    }

    void Update() {
        barCanvas.alpha = Mathf.Lerp(barCanvas.alpha, player.stamina < 0.99f ? 1 : 0, 60f * 0.05f * Time.deltaTime);

        if (barCanvas.alpha > 0.01f || player.stamina < 0.99f) {
            Vector2 pos = (Vector2)proj.WorldToScreenPoint(player.transform.position + new Vector3(0, -0.5f, 0)) - screenRect.sizeDelta / 2f;
            bar.anchoredPosition = pos;
            bari.fillAmount = player.stamina;
            bari.color = player.noStamina ? depletedColor : (player.stamina < 0.2f? warnColor : color);
            //transform.forward = cam.transform.forward;
        }
        else barCanvas.alpha = 0f;
    }
}
