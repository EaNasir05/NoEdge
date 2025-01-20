using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private bool deathState;
    private bool pauseState;
    private bool successState;
    public UnityEvent pause;
    public UnityEvent resume;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioClip successAudioClip;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        deathState = false;
        successState = false;
        pauseState = false;
        pause.AddListener(GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().OnPauseEvent);
        resume.AddListener(GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().OnResumeEvent);
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && deathState)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !deathState && !successState)
        {
            if (pauseState)
            {
                Time.timeScale = 1;
                resume.Invoke();
            }
            else
            {
                Time.timeScale = 0;
                pause.Invoke();
            }
            pauseState = !pauseState;
        }
    }

    public void OnDeathEvent()
    {
        SoundFXManager.instance.PlaySoundFXClip(deathAudioClip, transform, 0.7f);
        GameObject.FindGameObjectWithTag("MainCamera").transform.parent = null;
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Cube"));
        deathState = true;
    }

    public void OnSuccessEvent()
    {
        SoundFXManager.instance.PlaySoundFXClip(successAudioClip, transform, 0.7f);
        Time.timeScale = 0;
        successState = true;
    }
}
