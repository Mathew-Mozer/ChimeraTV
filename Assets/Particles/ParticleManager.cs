/* Particle Manager
 * Author - Stephen King
 * Created on 9/10/2015
 * Modified on 5/20/2015
 * Ver. 1.1
 * 
 * This plays all the particles loaded into the particles list. This class automatically
 * creates a static reference on awake, so usage should be easy.
 * 
 * Usage:
 * Simply call ParticleManager.LaunchFireworks() from anywhere within Unity. The burst delay can be set on the ParticleManager object.
 * Bursts can be added or removed from the particles list on the ParticleManager.
 * 
 * Note: Shuriken (the Unity particle system) does not have a completed interface and most of the system is private without any exposure
 * for runtime access. This means that 90% of the particle (including randomizing between to starting colors) must be set in the inspector
 * and set during compile time. Currently there is no way around this.
 * 
 * TODO
 * - Allow other particle lists to be called (for falling coins, dollar bills, etc)
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {

    public float minStartDelay;
    public float maxStartDelay;
    public bool StartFireworks;
    public GameObject congratswindow;
    public List<GameObject> particles = new List<GameObject>();

    public static ParticleManager instance = null;

    //When loaded, make an object for a static reference to the class. If one already exists,
    //destroy this instead.
  /*  void Awake() {
        if (instance == null) {
            instance = this;
        }else if(instance != this){
                Destroy(gameObject);
            }
        } */
    
    void Start() {

        //temp for testing. Comment this line of code before production
        
        //InvokeRepeating("LaunchFireworks", 2.0f, 10.0f);
            }

    //Where the magic happens
    public void LaunchFireworks()
    {
        StartCoroutine("StartThing");
    }
    public void LaunchTheFireworks() {
        
        foreach (GameObject particle in particles) {

            //Preload the particle system for the next 100ms
            ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();

            
            particle.transform.position = AssignRandomLocation();
            particleSystem.startDelay = AssignRandomDelay();
            particleSystem.Play();
            //particleSystem.Stop();
        }

    }

    //Assign a random world vector based on screen dimensions. This should make the particles
    //fit any screen dynamically.

    Vector3 AssignRandomLocation() {
        float randomHeight = Random.Range(0, Screen.height);
        float randomWidth = Random.Range(0, Screen.width);
        float randomDepth = Random.Range(-10, -1);

        //Debug.Log(randomHeight + " " + randomWidth + " " + randomDepth);

        Vector3 newVector = new Vector3(randomWidth, randomHeight, randomDepth);

        //Debug.Log("New Vector: " + newVector);
        return newVector = Camera.main.ScreenToWorldPoint(newVector);
    }

    //Randomly assigns a delay to the particle effects simulation startup. This ensures that the particles
    //do not all fire at the same exact time when the program begins or LaunchFireworks() is called.
    float AssignRandomDelay() {
        return Random.Range(minStartDelay, maxStartDelay);
    }
    private void Update()
    {
        if (StartFireworks)
        {
            StartFireworks = false;
            LaunchFireworks();
        }
    }


    private IEnumerator StartThing()
    {
        int count = 0;
        while (count < 5)
        {   LaunchTheFireworks();
            count++;
            yield return new WaitForSeconds(4);
            
        }
    }

    public void PauseFireworks() {

        foreach (GameObject particle in particles) {

            //Preload the particle system for the next 100ms
            ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();

            particleSystem.Pause();
        }
    }

    /// <summary>
    /// Resume paused particles
    /// </summary>

    public void ResumeFireworks()
    {

        foreach (GameObject particle in particles)
        {

            //Preload the particle system for the next 100ms
            ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();

            particleSystem.Play();
        }
    }   

}
