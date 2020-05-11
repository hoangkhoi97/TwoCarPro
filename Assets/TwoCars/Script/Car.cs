using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public bool isRedPlayer = true;

    private int totalClick = 1;

    public GameObject animationFollow;
    public void ChangeTheRoad(bool isRedBtn)
    {
        if(isRedBtn)
        {
            if(totalClick % 2==0)
            {
                AnimationClick(-2.127f, 45f);
            }
            else
            {
                AnimationClick(-0.7f, -45f);
            }
            totalClick++;
        }
        else
        {
            if (totalClick % 2 == 0)
            {
                AnimationClick(2.127f, -45f);
            }
            else
            {
                AnimationClick(0.7f, 45f);
          
            }
            totalClick++;
        }
    }

    private void AnimationClick(float postionEndX, float rotationZ)
    {
        Sequence sequence = DOTween.Sequence();
        this.transform.DOKill();
        this.transform.DOMoveX(postionEndX, 0.4f);
        sequence.Append(this.transform.DORotate(new Vector3(0, 0, rotationZ), 0.2f));
        sequence.Append(this.transform.DORotate(Vector3.zero, 0.15f));
    }

    //testing

    private void Update()
    {
        animationFollow.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.65f, 0);
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isRedPlayer)
        {
            ChangeTheRoad(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isRedPlayer)
        {
            ChangeTheRoad(false);
        }
    }
}
