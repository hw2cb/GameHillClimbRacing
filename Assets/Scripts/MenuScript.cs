using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject levelChanger;
    public GameObject exitWindow;
    public GameObject shopWindow;
    public GameObject[] buyCar;
    public Image[] cars;
    public Text money;
    private int moneys = 0;
    private string[] keyHaveCar = new string[3] {"startcar","havecar2", "havecar3" }; //ключи для сохранения купленой машины
    void Start()
    {
        PlayerPrefs.SetInt("Money", 2000);
        PlayerPrefs.SetInt("startcar", 1); //по дефолту ставим доступность стартовой машине
        
        //PlayerPrefs.SetInt("havecar2", 0);
        //PlayerPrefs.SetInt("havecar3", 0);
        moneys = PlayerPrefs.GetInt("Money");
        int i = PlayerPrefs.GetInt("car");
        cars[i].color = new Color(cars[i].color.r, cars[i].color.g, cars[i].color.b, 1f);
    }


    void Update()
    {
        money.text = moneys.ToString();
        if (levelChanger.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            levelChanger.SetActive(false);
        }
 
        else if (shopWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            shopWindow.SetActive(false);
        }
        for(int i=0; i<buyCar.Length; i++)
        {
            if(buyCar[i].activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                buyCar[i].SetActive(false);
                shopWindow.SetActive(true);
            }
        }

    }
    public void OnClickStart() //открываем окно магазина после нажатия кнопки Играть
    {
        shopWindow.SetActive(true);
    }
    public void OnClickShop() //открываем окно выбора уровня после выбора автомобиля
    {
        shopWindow.SetActive(false);
        levelChanger.SetActive(true);
    }
    public void OnClickExit() //открытие окна выхода
    {
        exitWindow.SetActive(true);

    }
    public void OnClickBackExit()
    {
        exitWindow.SetActive(false);
    }
    public void OnClickYesExit()
    {
        Application.Quit();
    }

    public void levelButtons(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    public void OnClickNotBuyCar()
    {
        for (int i = 0; i < buyCar.Length; i++)
        {
            buyCar[i].SetActive(false);
            shopWindow.SetActive(true);
        }

    }
    public void OnClickBuyCar()
    {
        if(buyCar[0].activeSelf && moneys>=300)
        {
            moneys = moneys - 300;
            PlayerPrefs.SetInt("Money", moneys);
            PlayerPrefs.SetInt("havecar2", 1);
            buyCar[0].SetActive(false);
            shopWindow.SetActive(true);
        }
        if (buyCar[1].activeSelf && moneys >= 500)
        {
            moneys = moneys - 500;
            PlayerPrefs.SetInt("Money", moneys);
            PlayerPrefs.SetInt("havecar3", 1);
            buyCar[1].SetActive(false);
            shopWindow.SetActive(true);
        }
    }
    //ключ havecar2 и havecar3
    public void carChanger(int car)
    {
        if (PlayerPrefs.GetInt(keyHaveCar[car]) == 1)//проверка на наличие машины 
        {
            PlayerPrefs.SetInt("car", car);
            PlayerPrefs.Save();
            for (int i = 0; i< cars.Length; i++)
            {
                cars[i].color = new Color(cars[i].color.r, cars[i].color.g, cars[i].color.b, 0.5f);
                cars[car].color = new Color(cars[car].color.r, cars[car].color.g, cars[car].color.b, 1f);
            }
        }
        else
        {
            shopWindow.SetActive(false);
            buyCar[car-1].SetActive(true);
        }

    }
}
