using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsDisplay : MonoBehaviour
{
    private Image[] stars; //массив всех звезд, в том числе и пустых
    private Button[] buttons;
    private MenuScript ms;
    public int levelChange = 0;
    public string keyName = "Stars1"; //по дефолту ключ от 1 уровня

    void Awake()
    {
        stars = GetComponentsInChildren<Image>(); //получаем в массив компоненты дочерние
        ms = Camera.main.GetComponent<MenuScript>();
        buttons = ms.levelChanger.GetComponentsInChildren<Button>();
    }
    void Start()
    {
        if(PlayerPrefs.GetInt(keyName) == 3)
        {
            int unlockLvl = levelChange + 1;
            buttons[unlockLvl].interactable = true;
            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255); //с 1 индекса, потому что unity считает за image еще и кнопку
            stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
            stars[3].color = new Color(stars[3].color.r, stars[3].color.g, stars[3].color.b, 255);
            stars[4].color = new Color(stars[4].color.r, stars[4].color.g, stars[4].color.b, 255);
            stars[5].color = new Color(stars[5].color.r, stars[5].color.g, stars[5].color.b, 255);
            stars[6].color = new Color(stars[6].color.r, stars[6].color.g, stars[6].color.b, 255);
        }
        else if (PlayerPrefs.GetInt(keyName) == 2)
        {
            int unlockLvl = levelChange + 1;
            buttons[unlockLvl].interactable = true;
            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
            stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
            stars[3].color = new Color(stars[3].color.r, stars[3].color.g, stars[3].color.b, 255);
            stars[4].color = new Color(stars[4].color.r, stars[4].color.g, stars[4].color.b, 255);         
        }
        else if (PlayerPrefs.GetInt(keyName) == 1)
        {
            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
            stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
        }
        else
        {
            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255); //с 1 индекса, потому что unity считает за image еще и кнопку
            stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
            stars[3].color = new Color(stars[3].color.r, stars[3].color.g, stars[3].color.b, 255);
            stars[4].color = new Color(stars[4].color.r, stars[4].color.g, stars[4].color.b, 0);
            stars[5].color = new Color(stars[5].color.r, stars[5].color.g, stars[5].color.b, 0);
            stars[6].color = new Color(stars[6].color.r, stars[6].color.g, stars[6].color.b, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
