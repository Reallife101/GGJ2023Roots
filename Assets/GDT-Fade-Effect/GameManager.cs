using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject levelEndFade;
    [Header("Next Level Name")]
    public string nextLevel;

    private Coroutine currentCoroutine;
    private bool loadingInProgess;
    private bool menuActive;
    private int littleTreesUsed;
    private int treesLeft;
    

    [Header("Menu Items")]
    private PlayerInputAsset input;
    public InputAction restart { get; private set; }
    public InputAction toggleMenu { get; private set; }
    public GameObject menu;
    public GameObject levelSelect;
    public TMP_Text treesUsedtext;
    public TMP_Text treesLeftText; 
    public LevelSelect levelSelector;
    public static GameManager gameManager;


    [SerializeField]
    AudioClip levelTransition;

    [SerializeField]
    AudioSource au;

    private void Awake()
    {
        
        input = new PlayerInputAsset();
        menuActive = false;
        loadingInProgess = false;
        StopAllCoroutines();
        restart = input.Player.Restart;
        toggleMenu = input.Player.ToggleMenu;
        littleTreesUsed = -1; 
        
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

        treesLeft = GameObject.FindGameObjectWithTag("Player").GetComponent<TreeController>().numChildrenLeft + 1;

        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else {
            gameManager.treesLeft = treesLeft; 
            gameManager.UpdateTreesLeft();
            gameManager.UpdateTreesUsed();
            Destroy(this);
        }

        UpdateTreesLeft();
        UpdateTreesUsed();

    }

    public void LevelSelect() {
        menu.SetActive(false);
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


    public void UpdateTreesUsed() {
        treesUsedtext.text = $"{++littleTreesUsed}";
    }

    public void UpdateTreesLeft() {
        treesLeftText.text = $"{--treesLeft}";
    }


    public void NextLevel()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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
        au.PlayOneShot(levelTransition, 1f);
        levelEndFade.SetActive(true);
        yield return new WaitForSeconds(levelEndFade.GetComponent<GDTFadeEffect>().timeEffect);
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator LoadLevel(int index)
    {
        levelEndFade.SetActive(true);
        au.PlayOneShot(levelTransition, 1f);
        yield return new WaitForSeconds(levelEndFade.GetComponent<GDTFadeEffect>().timeEffect);
        SceneManager.LoadScene(index);
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
