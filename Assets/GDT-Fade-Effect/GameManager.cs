using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject levelEndFade;
    [Header("Next Level Name")]
    public string nextLevel;

    private Coroutine currentCoroutine;
    private bool loadingInProgess;
    private bool menuActive;
    

    [Header("Menu Items")]
    private PlayerInputAsset input;
    public InputAction restart { get; private set; }
    public InputAction toggleMenu { get; private set; }
    public GameObject menu;
    public GameObject levelSelect;
    public LevelSelect levelSelector;
    public static GameManager gameManager; 

    private void Awake()
    {
        
        input = new PlayerInputAsset();
        menuActive = false;
        loadingInProgess = false;
        StopAllCoroutines();
        restart = input.Player.Restart;
        toggleMenu = input.Player.ToggleMenu;

        restart.started += restartBehavior =>
        {
            if (loadingInProgess)
            {
                return;
            }
            loadingInProgess = true;
            RestartLevel();
        };

        toggleMenu.started += menuBehavior =>
        {
            if (levelSelect.activeInHierarchy)
            {
                levelSelect.SetActive(false);
                menuActive = true;
            }

            menuActive = !menuActive;
            menu.SetActive(menuActive);

          

            if (menu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        };

        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }


    }

    public void LevelSelect() {
        menuActive = !menuActive;
        menu.SetActive(menuActive);
        levelSelect.SetActive(true);
        levelSelector.Restart();
    }

    public void LevelToLoad(string levelName) {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
    
        Time.timeScale = 1;

        currentCoroutine = StartCoroutine(LoadLevel(levelName));

    }

    public void NextLevel()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(LoadLevel(nextLevel));
    }

    public void RestartLevel()
    {

        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        menu.SetActive(false);
        Time.timeScale = 1;

        currentCoroutine = StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadLevel(string sceneToLoad)
    {
        levelEndFade.SetActive(true);
        yield return new WaitForSeconds(levelEndFade.GetComponent<GDTFadeEffect>().timeEffect);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnEnable()
    {
        restart.Enable();
        toggleMenu.Enable();
    }

    private void OnDisable()
    {
        restart.Disable();
        toggleMenu.Disable();
    }


    public void Exit() {
        Debug.Log("QUITTING");
        Application.Quit();
    
    }
}
