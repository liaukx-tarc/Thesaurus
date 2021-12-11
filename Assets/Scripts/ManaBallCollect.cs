using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBallCollect : MonoBehaviour
{
    string manaBallName;
    static public int manaBallNeeded;
    public TextMeshProUGUI manaText;
    public Image manabar;

    // Start is called before the first frame update
    void Start()
    {
        manaBallName = "ManaBall";
        manaText.text = WorldController.manaBallNum + " / " + manaBallNeeded;
        manabar.rectTransform.sizeDelta = new Vector2(0, manabar.rectTransform.sizeDelta.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == manaBallName)
        {
            other.gameObject.SetActive(false);
            WorldController.manaBallNum++;
            if(WorldController.manaBallNum <= manaBallNeeded)
            {
                manaText.text = WorldController.manaBallNum + " / " + manaBallNeeded;
                manabar.rectTransform.sizeDelta = new Vector2(System.Math.Min(manabar.rectTransform.sizeDelta.x
                    + ((float)546 / manaBallNeeded), 546),
                    manabar.rectTransform.sizeDelta.y);
            }
            
        }
    }
}
