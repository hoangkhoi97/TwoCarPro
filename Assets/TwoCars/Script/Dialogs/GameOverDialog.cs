using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDialog : BaseDialog
{
    public static GameOverDialog instance;

    public Text score;
    public Text bestScore;
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

    public override void showUI()
    {
        base.showUI();
        score.text = GameController.instance.score.ToString();
        if (PlayerPrefs.GetInt("bestScore") != null)
        {
            if(PlayerPrefs.GetInt("bestScore") < GameController.instance.score)
            {
                PlayerPrefs.SetInt("bestScore", GameController.instance.score);
                bestScore.text = score.text;
            }
            else
            {
                bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
            }
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", GameController.instance.score);
            bestScore.text = score.text;
        }
    }

    public void onClickRestart()
    {
        PlayerPrefs.SetString("isRestart", "true");
        onClickHome();
        
    }


}
