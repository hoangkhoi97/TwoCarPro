using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BaseDialog : MonoBehaviour
{
    public Transform panel;
//    public GameObject loading;
    public CanvasGroup canvas;

    public virtual void showUI()
    {
        GameController.instance.isPause = true;
        panel.gameObject.SetActive(true);
        canvas.DOFade(1f, 0.5f);
       // loading.GetComponent<Image>().DOFade(0, 1f).OnComplete(() => { Destroy(loading.GetComponent<Image>()); });
    }

    public virtual void closeUI()
    {
        canvas.DOFade(0f, 0.2f).OnComplete(() => {
            panel.gameObject.SetActive(false);
            GameController.instance.isPause = false;
        });
    }

    public void onClickHome()
    {
        SceneManager.LoadScene("GameScene");
    }
}
