using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    private GameObject particalRed, particalBlue;
    public SpriteRenderer sprite; // ispoint true
    public bool isRed = false;

    public bool isPoint = false;

    private float y = 0;
    private void Start()
    {
        particalRed = GameController.instance.particalRed;
        particalBlue = GameController.instance.particalBlue;

        y = this.transform.position.y;

    }

    private void Update()
    {
        if (GameController.instance.isGameOver || GameController.instance.isPause)
        {
            // not thing to do with postion y
        }
        else
        {
            y -= GameController.instance.scrollSpeed * Time.deltaTime;
            this.transform.position = new Vector2(this.transform.position.x, y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            if(isPoint)
            {
                GameController.instance.CarScored();
                Destroy(this.gameObject);
            }
            else{
                GameController.instance.GameOver();
                GameController.instance.AudioHit(isPoint);
                GameObject go = (isRed) ? particalRed : particalBlue;
                Instantiate(go, this.transform.position,Quaternion.identity);
               
                Destroy(this.gameObject);
            }
        }
        if (other.CompareTag("CheckPoint"))
        {
            if (isPoint)
            {
                GameController.instance.GameOver();
                if(sprite!=null)
                {
                    sprite.DOColor(new Color(1f, 1f, 1f, 0), 0.3f).SetLoops(6, LoopType.Yoyo);
                }
            }
        }
        if (other.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
}
