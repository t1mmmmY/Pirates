using UnityEngine;
using System.Collections;

public class InputController : Photon.MonoBehaviour
{
	//public static InputController inputControllerScript;

	public CameraController cameraController;
	public ShipController shipController;

	private Vector3 oldMousePos = Vector3.zero;
	private Vector3 deltaMousePos = Vector3.zero;


	private enum INPUT_STATE
	{
		NONE,
		MOVE_CAMERA,
		STEERING_WHEEL
	}

	private INPUT_STATE inputState = INPUT_STATE.NONE;


	void Awake()
	{

		//inputControllerScript = this;
		if (!photonView.isMine)
		{
			shipController.enabled = false;
			cameraController.gameObject.SetActive(false);
			this.enabled = false;
			this.GetComponent<ShipPhysic>().enabled = false;
			this.rigidbody.isKinematic = true;
			this.rigidbody.useGravity = false;
		}
	}


	void Update()
	{
		MoveCamera();
		SteeringWheel();
		ChangeSails();
	}

	private Vector3 shiftCamera;

	void MoveCamera()
	{
		if (inputState == INPUT_STATE.NONE || inputState == INPUT_STATE.MOVE_CAMERA)
		{

			if (Input.GetMouseButton(0))
			{
				Vector3 mousePos = Input.mousePosition;
				
				if (Screen.height - mousePos.y > Screen.height * 2.0f / 3.0f && inputState == INPUT_STATE.NONE)
				{
					return;
				}

				inputState = INPUT_STATE.MOVE_CAMERA;

				
				if (oldMousePos == Vector3.zero)
				{
					oldMousePos = mousePos;
				}
				
				deltaMousePos = (mousePos - oldMousePos) * Time.deltaTime;
				
				oldMousePos = mousePos;

				cameraController.ChangeShiftCamera(deltaMousePos);
			}
			
			if (Input.GetMouseButtonUp(0))
			{
				oldMousePos = Vector3.zero;
				deltaMousePos = Vector3.zero;
				inputState = INPUT_STATE.NONE;
			}

		}

	}


	float direction;

	void SteeringWheel()
	{
		
		if (Input.GetKeyDown(KeyCode.A))
		{
			TurnLeft();
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			TurnRight();
			
		}

		if (inputState == INPUT_STATE.NONE || inputState == INPUT_STATE.STEERING_WHEEL)
		{
		
			if (Input.GetMouseButton(0))
			{
				Vector3 mousePos = Input.mousePosition;
				
				if (mousePos.y < Screen.height * 1.0f / 3.0f)
				{
					inputState = INPUT_STATE.STEERING_WHEEL;

					if (oldMousePos == Vector3.zero)
					{
						oldMousePos = mousePos;
					}
					
					deltaMousePos = (mousePos - oldMousePos) * Time.deltaTime;
					
					oldMousePos = mousePos;
					
					//Debug.Log(deltaMousePos.x);
					if (Mathf.Abs(deltaMousePos.x) > 0.025f)
					{
						if (deltaMousePos.x < 0)
						{
							TurnLeft();
						}
						else
						{
							TurnRight();
						}
					}
					else
					{
						direction = 0;
					}
				}
				
			}
			if (Input.GetMouseButtonUp(0))
			{
				inputState = INPUT_STATE.NONE;
				oldMousePos = Vector3.zero;
				deltaMousePos = Vector3.zero;
			}
		}

		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			direction = 0;
		}

		shipController.ChangeSteeringDirection(direction);

	}

	void ChangeSails()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			shipController.Forward();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			shipController.Back();
		}
		
		
		//		Touch[] touches = Input.touches;
		//		if (touches.Length > 0)
		//		{
		//			if (touches[0].phase == TouchPhase.Moved)
		//			{
		//				float vertical = touches[0].deltaPosition.y * touches[0].deltaTime;
		//				//Debug.Log(vertical.ToString());
		//				if (Mathf.Abs(vertical) > 0.05f)
		//				{
		//					if (vertical < 0)
		//					{
		//						Forward();
		//					}
		//					else
		//					{
		//						Back();
		//					}
		//				}
		//
		//			}
		//		}
		
		//SAILS ANIMATION
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


	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, Screen.width / 10, Screen.height / 5), "+"))
		{
			shipController.Forward();
		}
		if (GUI.Button(new Rect(10, 30 + Screen.height / 5, Screen.width / 10, Screen.height / 5), "-"))
		{
			shipController.Back();
		}


		if (GUI.Button(new Rect(10, Screen.height - Screen.height / 5 - 20, Screen.width / 8, Screen.height / 5), "FIRE"))
		{
			shipController.ShotLeft();
		}
		if (GUI.Button(new Rect(Screen.width - Screen.width / 8 - 10, Screen.height - Screen.height / 5 - 20, Screen.width / 10, Screen.height / 5), "FIRE"))
		{
			shipController.ShotRight();
		}

	}

}
