using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    public int cellCount = 0;
    public Image cellImage;

    private void Start()
    {
        cellImage.enabled = false;
    }

    private void LateUpdate()
    {
        if(cellCount > 0)
        {
            cellImage.enabled = true;
        }
        else
        {
            cellImage.enabled = false;
        }
    }

    public void cellPickup()
    {
        cellCount++;
    }

    public void cellDeposit()
    {
        cellCount--;
    }
}
