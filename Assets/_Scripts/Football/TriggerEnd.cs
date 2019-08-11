using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerEnd : MonoBehaviour {

    [SerializeField]
    GameObject promoScreen;

    [SerializeField]
    GameObject footballField;

    [SerializeField]
    GameObject squadron;

    [SerializeField]
    GameObject football;

    [SerializeField]
    int reloadScene;

    /// <summary>
    /// Detect when the ball has landed
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other) {

        ResetScene();

        
        
    }

    /// <summary>
    /// End the scene by showing results and clearing 3d objects.
    /// </summary>
    public void ResetScene() {
        promoScreen.SetActive(true);
        footballField.SetActive(false);
        football.SetActive(false);
        squadron.SetActive(false);
        gameObject.SetActive(false);
        //Invoke("RestartKickScene", 10);
    }

    /// <summary>
    /// Restart the scene
    /// </summary>
    void RestartKickScene() {
        Debug.Log("Restring");
        SceneManager.LoadScene(reloadScene);
    }

   
}
