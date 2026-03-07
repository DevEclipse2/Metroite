using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class HudTips : MonoBehaviour
{
    public string OxyMaskHint;
    public string[] Tutorial;
    int index = 0;
    int currentindex = 0;
    public float fadeOutT;
    public float fadeInT;
    float timer;
    public List<string> Status;
    bool fadeout;
    bool fadein;
    TextMeshProUGUI tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    public void Look()
    {
        
        if(currentindex == 1 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void ToggleRot()
    {
        if (currentindex == 3 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void Move()
    {

        if(currentindex == 0 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void Scroll()
    {
        if (currentindex == 4 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void ScrollP()
    {
        if (currentindex == 6 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void Click()
    {
        if (currentindex == 5 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void RClick()
    {
        if (currentindex == 7 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void Space()
    {
        if (currentindex == 8 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }
    public void LookAsteroid()
    {
        if (currentindex == 2 && !fadein && !fadeout)
        {
            index++;
            fadeout = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout)
        {
            timer += Time.deltaTime;
            tmp.color = new UnityEngine.Color(1, 1, 1, 1 - (timer / fadeOutT));
            if (timer > fadeOutT)
            {
                timer = 0;
                fadeout = false;
                fadein = true;
            }
        }
        if (fadein)
        {
            currentindex = index;
            if(currentindex < Tutorial.Length)
            {
                tmp.text = Tutorial[currentindex];

            }
            else
            {
                tmp.text = "";
            }
            timer += Time.deltaTime;
            tmp.color = new UnityEngine.Color(1, 1, 1, (timer / fadeOutT));

            if (timer > fadeInT)
            {
                timer = 0;
                fadein = false;
            }
        }
    }
}
