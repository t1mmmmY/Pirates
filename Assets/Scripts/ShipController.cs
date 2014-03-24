using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public Transform steeringWheel;
	public Transform wheelPivot;
	public float wheelAngle = 0.0f;
	public float turnPower = 10.0f;
	public float maxWheelAngle = 1440;
	public float direction = 0;


	public float maxSpeed = 10;
	public int sailsLevel = 0;
	public int maxSails = 2;
	public float windForce = 0.0f;

//	private Vector3 oldMousePos = Vector3.zero;
//	private Vector3 deltaMousePos = Vector3.zero;


	public Sails[] sails;
	public Cannon[] cannonsLeft;
	public Cannon[] cannonsRight;


	public void ChangeSteeringDirection(float newDirection)
	{
		direction = newDirection;
	}


	public void ShotLeft()
	{
		foreach (Cannon cannon in cannonsLeft)
		{
			cannon.Fire();
		}
	}

	public void ShotRight()
	{
		foreach (Cannon cannon in cannonsRight)
		{
			cannon.Fire();
		}
	}

	void Start()
	{

		//InputController.inputControllerScript.shipController = this;
		Input.simulateMouseWithTouches = true;

		foreach (Sails s in sails)
		{
			s.CloseSails();
		}
	}

	void Update()
	{
		SteeringWheel();
		//ChangeSails();
	}

	void FixedUpdate()
	{
		Wind();
		Rotation();
	}

	void SteeringWheel()
	{

//		if (Input.GetKeyDown(KeyCode.A))
//		{
//			TurnLeft();
//		}
//		if (Input.GetKeyDown(KeyCode.D))
//		{
//			TurnRight();
//
//		}
//
//
//		if (Input.GetMouseButton(0))
//		{
//			Vector3 mousePos = Input.mousePosition;
//			
//			if (mousePos.y < Screen.height * 1.0f / 3.0f)
//			{
//				if (oldMousePos == Vector3.zero)
//				{
//					oldMousePos = mousePos;
//				}
//				
//				deltaMousePos = (mousePos - oldMousePos) * Time.deltaTime;
//				
//				oldMousePos = mousePos;
//				
//				Debug.Log(deltaMousePos.x);
//				if (Mathf.Abs(deltaMousePos.x) > 0.01f)
//				{
//					if (deltaMousePos.x < 0)
//					{
//						TurnLeft();
//					}
//					else
//					{
//						TurnRight();
//					}
//				}
//				else
//				{
//					direction = 0;
//				}
//			}
//
//		}
//		if (Input.GetMouseButtonUp(0))
//		{
//			oldMousePos = Vector3.zero;
//			deltaMousePos = Vector3.zero;
//		}

		
		if (direction != 0)
		{
			float oldAngle = wheelAngle;
			wheelAngle = Mathf.Lerp(wheelAngle, maxWheelAngle * Mathf.Abs(direction) / direction, Time.deltaTime * turnPower * Mathf.Abs(direction));
			
			steeringWheel.RotateAround(wheelPivot.position, wheelPivot.forward, wheelAngle - oldAngle);

			//RUDDER ANIMATION
		}
		
//		if (Input.GetKeyDown(KeyCode.Space))
//		{
//			direction = 0;
//		}
	}

//	void ChangeSails()
//	{
//		if (Input.GetKeyDown(KeyCode.W))
//		{
//			Forward();
//		}
//		if (Input.GetKeyDown(KeyCode.S))
//		{
//			Back();
//		}
//
//
////		Touch[] touches = Input.touches;
////		if (touches.Length > 0)
////		{
////			if (touches[0].phase == TouchPhase.Moved)
////			{
////				float vertical = touches[0].deltaPosition.y * touches[0].deltaTime;
////				//Debug.Log(vertical.ToString());
////				if (Mathf.Abs(vertical) > 0.05f)
////				{
////					if (vertical < 0)
////					{
////						Forward();
////					}
////					else
////					{
////						Back();
////					}
////				}
////
////			}
////		}
//
//		//SAILS ANIMATION
//	}

	void ChangeForce()
	{
		windForce = (float)sailsLevel / (float)maxSails * maxSpeed;

	}


	void Wind()
	{
		Vector3 forward = transform.forward;
		//forward.y = 0;
		rigidbody.AddForce(forward * windForce * rigidbody.mass);
	}

	void Rotation()
	{
		if (sailsLevel > 0)
		{
			rigidbody.AddTorque(transform.up * wheelAngle * turnPower * rigidbody.mass / sailsLevel);
		}
		else
		{
			rigidbody.AddTorque(transform.up * wheelAngle * turnPower * rigidbody.mass / 10);
		}
	}

	void TurnLeft()
	{
		if (direction > 0)
		{
			direction = 0;
		}
		if (direction > -3)
		{
			direction -= 1;
		}
	}

	void TurnRight()
	{
		if (direction < 0)
		{
			direction = 0;
		}
		if (direction < 3)
		{
			direction += 1;
		}
	}

	public void Forward()
	{
		if (sailsLevel < maxSails)
		{
			sailsLevel++;
			sails[sailsLevel-1].OpenSails();
			ChangeForce();
		}
	}

	public void Back()
	{
		if (sailsLevel > 0)
		{
			sailsLevel--;
			sails[sailsLevel].CloseSails();
			ChangeForce();
		}
	}


	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, Screen.width / 10, Screen.height / 5), "+"))
		{
			Forward();
		}
		if (GUI.Button(new Rect(10, 30 + Screen.height / 5, Screen.width / 10, Screen.height / 5), "-"))
		{
			Back();
		}
	}

}


[System.Serializable]
public class Sails
{
	public GameObject[] sail;

	public void OpenSails()
	{
		foreach (GameObject go in sail)
		{
			go.SetActive(true);
		}
	}

	public void CloseSails()
	{
		foreach (GameObject go in sail)
		{
			go.SetActive(false);
		}
	}

}
