using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarPhysics : MonoBehaviour
{

    WheelJoint2D[] wheelJoints;
    JointMotor2D frontWheel;
    JointMotor2D backWheel;

    public float maxSpeed = -1000f; // если с + то назад, если с минусом то вперед
    private float maxBackSpeed = 1500f; //скорость назад
    private float acceleration = 550f; //ускорение
    private float deacceleration = -100f; //замедление
    public float brakeForce = 3000f; //сила торможения
    private float gravity = 9.8f; //гравитация
    private float angleCar = 0; //угол наклона автомобиля

    public float fuelSize; //размер бака
    public float fuelMileage; //расход бензина
    private float currentFuel; //для прогрессбара
    public GameObject fuelProgressBar;
    public float wheelSize = 0.286f; //стандартный размер колеса первой машины
    public bool atGround = false; //на земле
    public LayerMask map;
    public Transform bwheel;
    public Text coinsText;
    public int coinsInt = 0;

    public ClickScript[] controlCar;
    public GameObject fp;
    public GameObject lp;
    // Start is called before the first frame update
    //при старте
    void Start()
    {
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        frontWheel = wheelJoints[0].motor; //0 потому что переднее колесо первое в массиве
        backWheel = wheelJoints[1].motor;

        currentFuel = fuelSize; //приравниваем прогресбар к размеру бака, когда машина стартует
    }

    void Update()
    {
        coinsText.text = coinsInt.ToString();
        atGround = Physics2D.OverlapCircle(bwheel.transform.position, wheelSize, map); //на земле ли колесо?

    }

    void FixedUpdate()
    {
        if(currentFuel <=0)
        {
            lp.SetActive(true);
            Debug.Log("Кончилось топливо");
        }
        frontWheel.motorSpeed = backWheel.motorSpeed; //приравняем переднее колесо к заднему
        angleCar = transform.localEulerAngles.z; //приравниваем углу автомобиля координату Rotation Z
        //баг с углом автомобиля
        if(angleCar >=180)
        {
            angleCar = angleCar - 360;
        }
        //газ
        if(atGround)
        {
            if(controlCar[0].isClicked)
            {
                currentFuel -= fuelMileage * Time.deltaTime; //минусуем расход у прогресбара
                print(currentFuel);
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity*Mathf.PI*(angleCar/2))*Time.deltaTime, maxSpeed, maxBackSpeed); //ограничитель, указаное значение не выйдет за максимальное и минимальное
            }
            if(((!controlCar[0].isClicked) && backWheel.motorSpeed<0)||((!controlCar[0].isClicked) && backWheel.motorSpeed ==0 && angleCar<0))
            {
                currentFuel -= (fuelMileage / 1.7f) * Time.deltaTime;
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (deacceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, maxSpeed, 0);
            }
            else if ((!controlCar[0].isClicked) && backWheel.motorSpeed>0 || ((!controlCar[0].isClicked) && backWheel.motorSpeed ==0 && angleCar >0))
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime,0, maxBackSpeed);
            }
        }
        else if((!controlCar[0].isClicked) && backWheel.motorSpeed<0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deacceleration*Time.deltaTime, maxSpeed,0);
        }
        else if((!controlCar[0].isClicked)&& backWheel.motorSpeed>0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + deacceleration * Time.deltaTime, 0, maxBackSpeed);
        }
        if (controlCar[0].isClicked && !atGround)
        {
            currentFuel -= (fuelMileage/1.4f) * Time.deltaTime; //если газ зажат но мы не на земле
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, maxSpeed, maxBackSpeed);
        }

        //тормоз
        if(controlCar[1].isClicked && backWheel.motorSpeed >0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce*Time.deltaTime, 0, maxBackSpeed);
        }
        else if(controlCar[1].isClicked && backWheel.motorSpeed <0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + brakeForce * Time.deltaTime, maxSpeed, 0);
        }
        wheelJoints[1].motor = backWheel; // скорость колес равна мощности двигателя
        wheelJoints[0].motor = frontWheel;

        fuelProgressBar.transform.localScale = new Vector2(currentFuel/fuelSize,1);
    }
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "coins")
        {
            coinsInt++;
            Destroy(trigger.gameObject);
        }
        else if (trigger.gameObject.tag == "finish")
        {
            fp.SetActive(true);
            Debug.Log("Финишировал");
        }
        else if(trigger.gameObject.tag == "fuel")
        {
            Destroy(trigger.gameObject);
            currentFuel = fuelSize;
        }

    }


}
