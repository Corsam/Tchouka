using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    GameManager gm;
    public EventSystem es;
    GameObject currentSelected;
    public AudioSource source;

    public AudioClip nav;
    public AudioClip sel;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        currentSelected = es.firstSelectedGameObject;
    }

    public void LoadScene(string scene)
    {
        gm.LoadLevel(scene);
    }

    public void QuitGame()
    {
        gm.QuitGame();
    }

    public void NavigationSound()
    {
        source.clip = nav;
        source.Play();
    }

    public void SelectionSound()
    {
        source.clip = sel;
        source.Play();
    }
}
