using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    public CarPhysics carScript;
    public Text coinsRes;
    public int coinsTarget3 = 15;
    public int coinsTarget2 = 10;
    public int coinsTarget1 = 5;
    public Image[] nullStars;
    public Image[] fullStars;
    public string keyNameStars = "Stars1";
    public GameObject[] cars;
    private SmoothCamera sc;
    private bool isPause = false;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1;
        carScript = cars[PlayerPrefs.GetInt("car")].GetComponent<CarPhysics>();
        sc = GetComponent<SmoothCamera>();
        cars[PlayerPrefs.GetInt("car")].SetActive(true);
        sc.target = cars[PlayerPrefs.GetInt("car")].transform;
        sc.speedCar = cars[PlayerPrefs.GetInt("car")].GetComponent<WheelJoint2D>();
    }


    void Update()
    {
        if(carScript.fp.activeSelf || carScript.lp.activeSelf) //отпимизация
        {
            Time.timeScale = 0;
            for (int i=0; i<carScript.controlCar.Length; i++) //отключение управления
            {
                carScript.controlCar[i].gameObject.SetActive(false);
                carScript.controlCar[i].isClicked = false;
            }
            coinsRes.text = "Собрано монет: "+ carScript.coinsInt.ToString();
            if(Input.GetMouseButtonDown(0))
            {
                int buff = 0;
                buff = PlayerPrefs.GetInt("Money");

                PlayerPrefs.SetInt("Money", buff + carScript.coinsInt);
                PlayerPrefs.Save();
                SceneManager.LoadScene(0);
            }
            if(carScript.coinsInt == coinsTarget3) // если выполнена цель на три звезды
            {
                PlayerPrefs.SetInt(keyNameStars, 3);
                nullStars[0].gameObject.SetActive(false);
                nullStars[1].gameObject.SetActive(false);
                nullStars[2].gameObject.SetActive(false);
                fullStars[0].gameObject.SetActive(true);
                fullStars[1].gameObject.SetActive(true);
                fullStars[2].gameObject.SetActive(true);
            }
            else if (carScript.coinsInt >= coinsTarget2) //на две
            {
                if (PlayerPrefs.GetInt(keyNameStars) != 3) //на тот случай, если игрок пройдет уже пройденый на 3 звезды уровень, что бы не присваивать 2 звезды
                {
                    PlayerPrefs.SetInt(keyNameStars, 2);
                }
                nullStars[0].gameObject.SetActive(false);
                nullStars[1].gameObject.SetActive(false);
                fullStars[0].gameObject.SetActive(true);
                fullStars[1].gameObject.SetActive(true);
            }
            else if(carScript.coinsInt >= coinsTarget1)//на одну
            {
                if (PlayerPrefs.GetInt(keyNameStars) != 3 && PlayerPrefs.GetInt(keyNameStars) != 2) // дабы не присваивать одну звезду если у игрока уже 2 или 3
                {
                    PlayerPrefs.SetInt(keyNameStars, 1);
                }
                nullStars[0].gameObject.SetActive(false);
                fullStars[0].gameObject.SetActive(true);
            }
            PlayerPrefs.Save();
        }
        //пауза
        if(Input.GetKeyDown(KeyCode.Escape)&& !isPause && !carScript.fp.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0; //время, которое учитывается при физике останавливается
            isPause = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }
        
    }
    //методы для привязки к кнопкам для паузы
    public void OnClickPause()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0; //время, которое учитывается при физике останавливается
        isPause = true;
    }
    public void OnClickHome()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1; //время, которое учитывается при физике останавливается
        isPause = false;
        SceneManager.LoadScene(0);
    }
    public void OnClickPauseBack()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
}
