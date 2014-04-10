using UnityEngine;
using System.Collections;

public class Capitan : Photon.MonoBehaviour 
{

	void Start () 
	{
		if (photonView.isMine)
		{
			Camera.main.GetComponent<CameraController>().steeringWheel = this.transform;
		}
	}

}
