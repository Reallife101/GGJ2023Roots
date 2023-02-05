using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelect : MonoBehaviour
{
    [SerializeField] private List<LevelImage> levels = new List<LevelImage>();
    [SerializeField] private Image currentImage;
    [SerializeField] private TMP_Text level_name;
    [SerializeField] private GameObject backBtn;
    [SerializeField] private GameObject nextBtn;

    private int index;

    public void Restart()
    {
        index = 0;
        nextBtn.SetActive(true);
        backBtn.SetActive(false);
        SwitchInfo();
    }


    public void Select() {
        GameManager.gameManager.LevelToLoad(levels[index].image_name);
    
    }


    private void IndexCheckDisplay() {

        if (index == 0)
        {
            BtnDisplay(backBtn);

        }
        else { 
            if (!backBtn.activeInHierarchy)
                BtnDisplay(backBtn);

            if (index == levels.Count - 1)
            {
                BtnDisplay(nextBtn);

            }
                
        }
    }

    public void Next() {
        if (index >= levels.Count - 1)
            return;

        index++;

        IndexCheckDisplay();
        SwitchInfo();
    }

    public void Back() {
        if (index <= 0)
            return;

        --index;
       
        IndexCheckDisplay();
        SwitchInfo();
    }


    public void BtnDisplay(GameObject btn)
    {
        btn.SetActive(!btn.activeInHierarchy);
    }


    public void SwitchInfo()
    {
        currentImage.color = levels[index].image.color;
        level_name.text = levels[index].image_name;
    }


}
