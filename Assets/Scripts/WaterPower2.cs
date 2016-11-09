using UnityEngine;
using System.Collections;

//@author Nathan Young
public class WaterPower2 : MonoBehaviour {

	public GameObject waterParticleSystemPrefab;
	public GameObject waterParticleSystem;
	public GameObject blob;
	private ParticleSystem particleSystem;


	//Also see particle system setting: start lifetime
	public const float TIME_ACTIVE = .3f;

	//http://answers.unity3d.com/questions/225213/c-countdown-timer.html
	private float timeLeft; //time left for power on
	private bool canUseWater = true;
	private bool waterIsActive = true;
	private Vector3 blobPosition;


	//TODO: Create a particle system in front of the blob instead of always using the same one?
	//This will allow multiple water particle systems to exist

	// Use this for initialization
	void Start () {
		blob = GameObject.Find ("ActualBlob");



	}

	// Update is called once per frame
	void Update () {


		//if button pushed and blob in a state where it can use water
		if (Input.GetButton("WaterPower") && canUseWater && !waterIsActive) {
			waterParticleSystem = Instantiate (waterParticleSystemPrefab) as GameObject;
			particleSystem = waterParticleSystem.GetComponent<ParticleSystem> ();
			waterParticleSystem.SetActive (true);
			blobPosition = blob.transform.position + (blob.transform.forward*2);
			particleSystem.Play ();
			waterIsActive = true;



			timeLeft = TIME_ACTIVE;

			//if the water particle system is not already on, turn it on
			//if (!particleSystem.isPlaying) {
				particleSystem.Play();
				print ("ok");
			//}
		} else{
			timeLeft -= Time.deltaTime;
			//if player hasn't used water in a while it stops being active
			if(timeLeft<0 ){

				//changes the position of the particle system to be what the blob was
				particleSystem.transform.position = blobPosition;
				//particleSystem.Stop (); //  gameObject.GetComponent<ParticleSystem>().enableEmission = false; maybe use this, but its deprecated
				// above source is from http://answers.unity3d.com/questions/37875/turning-the-particle-system-on-and-off.html
			}


		}

	}

	private IEnumerator ShootWater(Vector3 position){
		yield return new WaitForSeconds (TIME_ACTIVE);
	}

}
