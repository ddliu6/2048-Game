using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2048 : MonoBehaviour
{
    public Cell2048 up;
    public Cell2048 down;
    public Cell2048 left;
    public Cell2048 right;
    public Fill2048 fill;

    private void OnEnable()
    {
        GameController2048.slide += onSlide;
    }

    private void OnDisable()
    {
        GameController2048.slide -= onSlide;
    }

    private void onSlide(string Sent)
    {
        CellCheck();
        Debug.Log(Sent);
        if(Sent == "w")
        {
            if (up != null) //move to the first one
                return;
            SlideUp(this);
        }
        else if(Sent == "a")
        {
            if (left != null)
                return;
            SlideLeft(this);
        }
        else if(Sent == "s")
        {
            if (down != null)
                return;
            SlideDown(this);
        }
        else if(Sent == "d")
        {
            if (right != null)
                return;
            SlideRight(this);
        }
        
        //Random spawning after each slide
        ++GameController2048.ticker;
        if (GameController2048.ticker == 4)
            GameController2048.instance.SpawnFill();
    }

    void SlideUp(Cell2048 curCell)
    {
        if (curCell.down == null)
            return;

        if (curCell.fill != null)
        {
            Cell2048 nextCell = curCell.down;
            //move to the filled/last cell
            while(nextCell.down != null && nextCell.fill == null)
                nextCell = nextCell.down;
            if(nextCell.fill != null)
            {
                if(curCell.fill.value == nextCell.fill.value)
                {
                    //combine 2 
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = curCell.transform;
                    curCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(curCell.down.fill != nextCell.fill)
                {
                    Debug.Log("!Doubled");
                    nextCell.fill.transform.parent = curCell.down.transform;
                    curCell.down.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = curCell.down;
            //move to the filled/last cell
            while (nextCell.down != null && nextCell.fill == null)
                nextCell = nextCell.down;
            if (nextCell.fill != null)
            {
                Debug.Log("Slide to Empty");
                nextCell.fill.transform.parent = curCell.transform;
                curCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideUp(curCell);
            }
        }

        if (curCell.down == null)
            return;
        //keep tracking down
        SlideUp(curCell.down);
    }

    void SlideDown(Cell2048 curCell)
    {
        if (curCell.up == null)
            return;

        if (curCell.fill != null)
        {
            Cell2048 nextCell = curCell.up;
            //move to the filled/last cell
            while (nextCell.up != null && nextCell.fill == null)
                nextCell = nextCell.up;
            if (nextCell.fill != null)
            {
                if (curCell.fill.value == nextCell.fill.value)
                {
                    //combine 2 
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = curCell.transform;
                    curCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (curCell.up.fill != nextCell.fill)
                {
                    Debug.Log("!Doubled");
                    nextCell.fill.transform.parent = curCell.up.transform;
                    curCell.up.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = curCell.up;
            //move to the filled/last cell
            while (nextCell.up != null && nextCell.fill == null)
                nextCell = nextCell.up;
            if (nextCell.fill != null)
            {
                Debug.Log("Slide to Empty");
                nextCell.fill.transform.parent = curCell.transform;
                curCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideDown(curCell);
            }
        }

        if (curCell.up == null)
            return;
        //keep tracking up
        SlideDown(curCell.up);
    }

    void SlideLeft(Cell2048 curCell)
    {
        if (curCell.right == null)
            return;

        if (curCell.fill != null)
        {
            Cell2048 nextCell = curCell.right;
            //move to the filled/last cell
            while (nextCell.right != null && nextCell.fill == null)
                nextCell = nextCell.right;
            if (nextCell.fill != null)
            {
                if (curCell.fill.value == nextCell.fill.value)
                {
                    //combine 2 
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = curCell.transform;
                    curCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (curCell.right.fill != nextCell.fill)
                {
                    Debug.Log("!Doubled");
                    nextCell.fill.transform.parent = curCell.right.transform;
                    curCell.right.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = curCell.right;
            //move to the filled/last cell
            while (nextCell.right != null && nextCell.fill == null)
                nextCell = nextCell.right;
            if (nextCell.fill != null)
            {
                Debug.Log("Slide to Empty");
                nextCell.fill.transform.parent = curCell.transform;
                curCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideLeft(curCell);
            }
        }

        if (curCell.right == null)
            return;
        //keep tracking right
        SlideLeft(curCell.right);
    }

    void SlideRight(Cell2048 curCell)
    {
        if (curCell.left == null)
            return;

        if (curCell.fill != null)
        {
            Cell2048 nextCell = curCell.left;
            //move to the filled/last cell
            while (nextCell.left != null && nextCell.fill == null)
                nextCell = nextCell.left;
            if (nextCell.fill != null)
            {
                if (curCell.fill.value == nextCell.fill.value)
                {
                    //combine 2 
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = curCell.transform;
                    curCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (curCell.left.fill != nextCell.fill)
                {
                    Debug.Log("!Doubled");
                    nextCell.fill.transform.parent = curCell.left.transform;
                    curCell.left.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = curCell.left;
            //move to the filled/last cell
            while (nextCell.left != null && nextCell.fill == null)
                nextCell = nextCell.left;
            if (nextCell.fill != null)
            {
                Debug.Log("Slide to Empty");
                nextCell.fill.transform.parent = curCell.transform;
                curCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideRight(curCell);
            }
        }

        if (curCell.left == null)
            return;
        //keep tracking left
        SlideRight(curCell.left);
    }

    void CellCheck()
    {
        //check if movable
        if (fill == null)
            return;
        if (up != null)
        {
            if (up.fill == null)
                return;
            if (up.fill.value == fill.value)
                return;
        }
        if (down != null)
        {
            if (down.fill == null)
                return;
            if (down.fill.value == fill.value)
                return;
        }
        if (left != null)
        {
            if (left.fill == null)
                return;
            if (left.fill.value == fill.value)
                return;
        }
        if (right != null)
        {
            if (right.fill == null)
                return;
            if (right.fill.value == fill.value)
                return;
        }
        GameController2048.instance.GameOverCheck();
    }
}
