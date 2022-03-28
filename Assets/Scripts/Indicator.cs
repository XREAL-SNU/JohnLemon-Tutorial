using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Indicator : MonoBehaviour {
    TextMeshProUGUI text;
    //MeshRenderer mr;
    WaypointPatrol patrol;
    GameObject cam, parent;
    Camera proj;
    RectTransform screenRect;

    private const float screenBorder = 40f;
    public Color color, aggroColor;

    void Start() {
        parent = transform.parent.gameObject;
        cam = FindObjectOfType<Camera>().gameObject;
        proj = cam.GetComponent<Camera>();
        Canvas canvas = FindObjectOfType<Canvas>();
        screenRect = canvas.GetComponent<RectTransform>();
        transform.SetParent(canvas.transform, false);
        transform.SetAsFirstSibling();

        text = GetComponent<TextMeshProUGUI>();
        //mr = GetComponent<MeshRenderer>();
        patrol = parent.GetComponent<WaypointPatrol>();

        text.enabled = false;
    }

    void Update() {
        text.enabled = patrol.observer.aggroing || patrol.observer.aggro > 0.1f || patrol.target != null;

        if (text.enabled) {
            if (patrol.observer.aggroing) {
                text.color = aggroColor;
                text.text = "!";
            }
            else {
                text.color = color;
                text.text = "?";
            }

            Vector2 pos = (Vector2)proj.WorldToScreenPoint(parent.transform.position + new Vector3(0, 1.5f, 0)) - screenRect.sizeDelta / 2f;
            pos.x = Mathf.Clamp(pos.x, screenBorder - screenRect.sizeDelta.x / 2f, -screenBorder + screenRect.sizeDelta.x / 2f);
            pos.y = Mathf.Clamp(pos.y, screenBorder - screenRect.sizeDelta.y / 2f, -screenBorder + screenRect.sizeDelta.y / 2f);

            text.rectTransform.anchoredPosition = pos;
            text.fontSize = 45f + 5f * Mathf.Sin(Time.time * 3.5f);
            //transform.forward = cam.transform.forward;
        }
    }
}
