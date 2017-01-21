using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public static CameraController i;

	Vector2 startPosition;
	float shakeTimer;
	float intensity;

	// Use this for initialization
	void Start () {
		i = this;
		startPosition = gameObject.transform.position;
	}

	void Update(){
		if (shakeTimer > 0) {
			shakeTimer -= Time.deltaTime;
			ScreenShakeUpdate ();
		} else {
			gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, startPosition, .1f);
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, -10f);
		}
	}

	public void ScreenShake(float duration, float i){
		shakeTimer = duration;
		intensity = i;
	}

	public void ScreenShakeUpdate(){
		gameObject.transform.position = new Vector3 (Mathf.Clamp(gameObject.transform.position.x + Random.Range(-intensity, intensity),gameObject.transform.position.x - intensity,gameObject.transform.position.x + intensity), Mathf.Clamp(gameObject.transform.position.y + Random.Range(-intensity, intensity),gameObject.transform.position.y - intensity,gameObject.transform.position.y + intensity), -10f);
	}
}
