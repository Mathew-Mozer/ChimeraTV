using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <author>Stephen King</author>
/// <date>6/22/2016</date>
/// <version>1.0</version>
/// 
/// This class allows background particles to be stored and toggled at will.
/// </summary>

public class BackgroundParticleManager : MonoBehaviour
{

    //These are the constantly active particle effects
    public List<GameObject> backgroundParticles;

    //The currently active particle
    public GameObject activeBackgroundParticle { get; set; }

    /// <summary>
    /// This deactivates the currently active background particle (if any) and activates the selected particle effect
    /// </summary>
    /// <param name="s_particleName"></param>
    public void ActivateBackgroundParticle(int particleID)
    {
        //Debug.Log("Activating: " + particleID);
        //If there is currently an active particle, deactivate it.
        if (activeBackgroundParticle != null)
        {
            activeBackgroundParticle.SetActive(false);
        }

        //Activate Particle
        if (particleID <= backgroundParticles.Count-1)
        {
            //Debug.Log("Activating: " + particleID);
            activeBackgroundParticle = backgroundParticles[particleID];
            backgroundParticles[particleID].SetActive(true);
        }else
        {
            //Debug.Log("Trying to Activate: " + particleID + " But the count is:" + backgroundParticles.Count);
        }

    }


}
