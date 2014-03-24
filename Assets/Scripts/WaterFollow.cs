using UnityEngine;
using System.Collections;

public class WaterFollow : MonoBehaviour 
{
	public Transform target;

	void FixedUpdate()
	{
		this.transform.position = new Vector3(target.position.x, this.transform.position.y, target.position.z);
	}
}
