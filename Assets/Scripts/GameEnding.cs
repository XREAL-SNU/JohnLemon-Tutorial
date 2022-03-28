using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    bool m_IsPlayerExit;
    bool m_IsPlayerCaught;
    float m_Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsPlayerExit) {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        } else if(m_IsPlayerCaught) {
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            m_IsPlayerExit = true;
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart) {
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if(m_Timer > fadeDuration + displayImageDuration) {
            if(doRestart) {
                SceneManager.LoadScene(0);
            } else {
                Application.Quit();
            }
        }
    }

    public void CaughtPlayer() {
        m_IsPlayerCaught = true;
    }
}
