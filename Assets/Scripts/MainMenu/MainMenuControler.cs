using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControler : MonoBehaviour
{
    public GameObject player;
    public Button Play;
    public GameObject LevelPanel;
    public Animator LevelPanelAnim;

    public void PlayGame()
    {
        LevelPanel.SetActive(true);
        LevelPanelAnim.Play("SlideIn");
        player.SetActive(false);
        Play.gameObject.SetActive(false);
    }

    public void Back()
    {
        LevelPanelAnim.Play("SlideOut");
        Invoke("ActivatingObj", .55f);
    }
    
    void ActivatingObj()
    {
        player.SetActive(true);
        Play.gameObject.SetActive(true);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(3);
    }
}
