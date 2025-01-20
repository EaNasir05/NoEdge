using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject successScreen;

    public void OnDeathEvent()
    {
        deathScreen.SetActive(true);
    }

    public void OnPauseEvent()
    {
        pauseScreen.SetActive(true);
    }

    public void OnResumeEvent()
    {
        pauseScreen.SetActive(false);
    }

    public void OnSuccessEvent()
    {
        successScreen.SetActive(true);
    }
}
