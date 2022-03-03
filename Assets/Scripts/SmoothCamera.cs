using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {
	public float dampTime = 0.15f; //сглаживание
	private Vector3 velocity = Vector3.zero;
    public Transform target; //цель

    public float offsetx = 0f;
    public float offsety = 0f;
    public GameObject background;
    public WheelJoint2D speedCar;
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x, target.position.y+0.75f,target.position.z));
			Vector3 delta = new Vector3(target.position.x +offsetx, target.position.y+0.75f+offsety,target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
        Vector3 posBackground = new Vector3(background.transform.position.x, background.transform.position.y, background.transform.position.z);
        if(speedCar.motor.motorSpeed<=-350)
        {
            posBackground.x = Camera.main.transform.position.x + 1f;
        }
        else if (speedCar.motor.motorSpeed >=150)
        {
            posBackground.x = Camera.main.transform.position.x - 1f;
        }
        
        background.transform.position = Vector3.SmoothDamp(background.transform.position, posBackground,ref velocity, 1f); //плавнео изменение позиции 
	}
}
