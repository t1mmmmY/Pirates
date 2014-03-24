using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform steeringWheel;
	public float speed = 10;
	public float rotationSpeed = 10.0f;
	public float speedShift = 2.0f;

	private Vector3 shiftCamera;
	//private Vector3 oldMousePos = Vector3.zero;
	//private Vector3 deltaMousePos = Vector3.zero;

//	void Update()
//	{
//		MoveCamera();
//	}

	public void ChangeShiftCamera(Vector3 deltaMousePos)
	{
		shiftCamera += new Vector3(deltaMousePos.y * speedShift, -deltaMousePos.x * speedShift, 0);
	}

	void FixedUpdate ()
	{   
		iTween.MoveUpdate(gameObject, steeringWheel.position, speed);
		iTween.RotateUpdate(gameObject, steeringWheel.rotation.eulerAngles + shiftCamera, rotationSpeed);
	}

//	void MoveCamera()
//	{
//		if (Input.GetMouseButton(0))
//		{
//			Vector3 mousePos = Input.mousePosition;
//
//			if (Screen.height - mousePos.y > Screen.height * 2.0f / 3.0f)
//			{
//				return;
//			}
//
//			if (oldMousePos == Vector3.zero)
//			{
//				oldMousePos = mousePos;
//			}
//
//			deltaMousePos = (mousePos - oldMousePos) * Time.deltaTime;
//
//			oldMousePos = mousePos;
//
//			shiftCamera += new Vector3(deltaMousePos.y * speedShift, -deltaMousePos.x * speedShift, 0);
//		}
//
//		if (Input.GetMouseButtonUp(0))
//		{
//			oldMousePos = Vector3.zero;
//			deltaMousePos = Vector3.zero;
//		}
//
////		if (Input.touchCount > 0)
////		{
////
////		}
//	}

}
