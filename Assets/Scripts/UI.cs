using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject insMenu;
    [SerializeField] GameObject startButton;

    public void Starting()
    {
        startButton.SetActive(false);
        EventManager.Instance.OnMissionDone.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        Application.Quit();
    }
    
    public void InsExit()
    {
        insMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void Instruction()
    {
        menu.SetActive(false);
        insMenu.SetActive(true);
    }
}
