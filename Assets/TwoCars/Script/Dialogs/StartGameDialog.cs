using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameDialog : BaseDialog
{

    public static StartGameDialog instance;

    public Toggle music, sound; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sound.onValueChanged.AddListener(CheckBoxSound);
        music.onValueChanged.AddListener(CheckBoxMusic);
    }


    private void CheckBoxSound(bool value)
    {
        if(value)
        {
            GameController.instance.isSound = false;
        }
        else
        {
            GameController.instance.isSound = true;
        }
    }

    private void CheckBoxMusic(bool value)
    {
        if (value)
        {
            GameController.instance.audioSource.Stop();
        }
        else
        {
            
            GameController.instance.audioSource.Play();
        }
    }

    public override void showUI()
    {
        if (PlayerPrefs.GetString("isRestart") != null)
        {
            if (PlayerPrefs.GetString("isRestart") == "true")
                PlayerPrefs.SetString("isRestart", "false");
            else
            {
                base.showUI();
            }
        }
        else
        {
            base.showUI();
        }
    }

    public override void closeUI()
    {
        if (PlayerPrefs.GetString("isTutorial") != null)
        {

            if (PlayerPrefs.GetString("isTutorial") == "false")
            {

                base.closeUI();
            }
            else
            {
                newCloseUI();
            }
        }
        else
        {
            newCloseUI();
        }   
    }

    private void newCloseUI()
    {
        canvas.DOFade(1f, 0.2f).OnComplete(() => {
            panel.gameObject.SetActive(false);
            GameController.instance.isPause = false;
            TutorialDialog.instance.showUI();
            PlayerPrefs.SetString("isTutorial", "false");
        });
    }


}
