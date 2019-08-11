using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyingCard : MonoBehaviour {

    public int renderQueue = 3000;
    public List<ParticleSystem> sys = new List<ParticleSystem>();
    Renderer ren; // = sys.GetComponent<Renderer>();
    Material mMat;
   public TweenScale aura1;
   public UISprite auraSprite;
    public TweenScale aura2;

    void Start() {
     
        if (ren == null) {
            foreach (ParticleSystem part in sys) {
                if (sys != null) ren = part.GetComponent<Renderer>();

                if (ren != null) {
                    mMat = new Material(ren.sharedMaterial);
                    mMat.renderQueue = renderQueue;
                    ren.material = mMat;
                }
            }
        }

        aura1.PlayForward();
        //Invoke("StartAura2", .5f);

        Invoke("StartMovement", 3);

        
    }

    void OnDestroy() { 
        if (mMat != null) Destroy(mMat); 
    }

   public void HideCard() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
       
    }

   public void StopTrail() {
       sys[0].Stop();
       sys[1].Stop();
       auraSprite.enabled = false;
       sys[2].Play();

   }

   public void StartTrail() {
       
       sys[0].Play();
       sys[1].Play();
       sys[2].Stop();

   }

   public void InitiateSelfDestruct() {
       Invoke("SelfDestruct", 2);
   }

   void SelfDestruct() {
       Destroy(gameObject);
   }

   void StartMovement() {
       GetComponent<TweenPosition>().PlayForward();
       GetComponent<TweenScale>().PlayForward();
   }

   void StartAura2() {
       aura2.PlayForward();
   }
}
