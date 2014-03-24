using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
	public GameObject effect;
	public AudioClip sound;
	public GameObject cannonball;

	public float power = 24;
	public float forceCoef = 5.0f;
	public Transform world; //WTF

	private ParticleSystem _effect;
	private GameObject _cannonball;

	void Start()
	{
		GameObject go = (GameObject)Instantiate(effect);
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;

		_effect = go.particleSystem;

		this.gameObject.AddComponent<AudioSource>();

		audio.clip = sound;

		go = (GameObject)Instantiate(cannonball);
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;

		_cannonball = go;
		_cannonball.SetActive(false);
	}

	public void Fire()
	{
		_effect.Play();

		audio.Play();

		StopCoroutine("DisableCannonball");

		_cannonball.rigidbody.velocity = Vector3.zero;
		_cannonball.rigidbody.angularVelocity = Vector3.zero;

		_cannonball.transform.parent = this.transform;
		_cannonball.transform.localPosition = Vector3.zero;
		_cannonball.transform.localRotation = Quaternion.identity;

		_cannonball.SetActive(true);
		_cannonball.transform.parent = world;

		//_cannonball.rigidbody.AddRelativeForce(this.transform.forward * power * 10, ForceMode.Impulse);
		_cannonball.rigidbody.AddForce(this.transform.forward * power * forceCoef, ForceMode.Impulse);

		StartCoroutine("DisableCannonball", 2.0f);
	}

	IEnumerator DisableCannonball(float time)
	{
		yield return new WaitForSeconds(time);

		_cannonball.SetActive(false);
	}
}
