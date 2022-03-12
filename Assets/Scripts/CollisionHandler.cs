using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    
    

    AudioSource audioSource;

    //bool isTransitioning = false;
    bool collisionDisable = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update(){
        RespondToDebugKeys();
    }
    void RespondToDebugKeys(){
        if (Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            collisionDisable = !collisionDisable;

        }
    }
    void OnCollisionEnter(Collision other) {
        //if (isTransitioning == false) {return;}
        switch(other.gameObject.tag)
        {
            case "Frendly":
                Debug.Log("This thing in friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }
    void StartSuccessSequence(){
        //isTransitioning = true;
        //audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence(){
        //isTransitioning = true;
       // audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
