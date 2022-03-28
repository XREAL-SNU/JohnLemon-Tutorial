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
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_IsPlayerExit;
    bool m_IsPlayerCaught;
    bool m_HasAudioPlayed;
    float m_Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsPlayerExit) {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        } else if(m_IsPlayerCaught) {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            m_IsPlayerExit = true;
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource) {
        if(!m_HasAudioPlayed) {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
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
