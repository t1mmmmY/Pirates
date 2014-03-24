using UnityEngine;
using System.Collections;

public class ShipPhysic : MonoBehaviour 
{
	public float waterLevel = 0.0f;
	public float floatHeight = 1.0f;
	public float bounceDamp = 0.05f;
	public float raycastHeightOffset = 10;
	public Transform[] bPoints;

	public float waterline = 0.9f;

	public Transform COM;
	private Vector3 _COM;
	//Fa = RoGV

	void Start () 
	{
		if (bPoints == null) 
		{
			bPoints = new Transform[1];
			bPoints[0].transform.position = transform.position;
		}

		StartCoroutine("ChangeCOM");
	}
	
	void FixedUpdate () 
	{
		for (int i = 0; i < bPoints.Length; i++) 
		{
			
			Vector3 actionPoint = bPoints[i].transform.position;
			
			Vector3 rayOrigin = actionPoint + new Vector3(0, raycastHeightOffset, 0);
			RaycastHit hit;
			int layerMask = 1 << 4;
			
			if (Physics.Raycast(rayOrigin, -Vector3.up, out hit, Mathf.Infinity, layerMask)) 
			{
				waterLevel = hit.point.y;
			}
			
			float forceFactor = ((1.0f - ((actionPoint.y - waterLevel) / floatHeight)) / bPoints.Length);
			
			if (forceFactor > 0.0f) 
			{
				Vector3 uplift = -Physics.gravity * (forceFactor - rigidbody.velocity.y * ((bounceDamp / bPoints.Length) * Time.deltaTime));
				uplift *= rigidbody.mass * waterline;
				rigidbody.AddForceAtPosition(uplift, actionPoint);
			}
		}
	}

	IEnumerator ChangeCOM()
	{
		do
		{
			if (_COM != COM.position)
			{
				_COM = COM.position;

				rigidbody.centerOfMass = COM.localPosition;
			}

			yield return new WaitForSeconds(1.0f);

		} while (Application.isPlaying);
	}

}
