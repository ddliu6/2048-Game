using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill2048 : MonoBehaviour
{
    [SerializeField] Text valueDisplay;
    [SerializeField] float speed;

    public int value;
    bool isCombined;
    Image myImage;

    void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            //move the fill obj
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
            isCombined = false;
        }
        else if(!isCombined)
        {
            //delete the extra filled obj
            if(transform.parent.GetChild(0) != this.transform)
                Destroy(transform.parent.GetChild(0).gameObject);
            isCombined = true;
        }
    }

    //fill value update
    public void FillValueUpdate(int valueIn)
    {
        value = valueIn;
        //valueDisplay.text = value.ToString();
        myImage = GetComponent<Image>();
        myImage.sprite = GameController2048.instance.fillSprites[GetSpriteIndex(value)];
    }

    public void Double()
    {
        //update the value
        value *= 2;
        FillValueUpdate(value);
        GameController2048.instance.ScoreUpdate(value);
        //win check
        GameController2048.instance.WinningCheck(value);
    }

    int GetSpriteIndex(int valueIn)
    {
        int index = -1;
        while(valueIn != 1)
        {
            ++index;
            valueIn /= 2;
        }
        return index;
    }
}
