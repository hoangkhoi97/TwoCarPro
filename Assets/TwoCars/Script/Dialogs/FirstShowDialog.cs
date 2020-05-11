using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstShowDialog : BaseDialog
{
    void Start()
    {
        showUI();
        Invoke("CloseDialog", 2.5f);
    }

    public override void showUI()
    { 
        panel.gameObject.SetActive(true);
        canvas.DOFade(1f, 1f);
    }

    void CloseDialog()
    {
        canvas.DOFade(0f, 0.5f).OnComplete(() => {
            SceneManager.LoadScene("GameScene");
        });
    }
}
