using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    bool isTransition = false;
    bool collisionDisable = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        RespondToDebugKeys();//Cheats
    }

    void RespondToDebugKeys() {
        if(Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C)) {
            collisionDisable = !collisionDisable;
        }
    }
    void OnCollisionEnter(Collision other)
    { //by default methods are private so no need of private keyword before void
        if(!isTransition && !collisionDisable) {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This is Friendly!");
                    break;
                case "Finish":
                    // Debug.Log("Congrats, you have finished!");
                    StartSuccessSequence();
                    break;
                case "Fuel":
                    Debug.Log("You have oicked up a Fuel!");
                    break;
                default:
                    // Debug.Log("Sorry, You blew Up!");
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartSuccessSequence()
    {
        isTransition = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay); //invoke delays the execution of the method given as first parameter by x seconds given in argument as second param
    }

    void StartCrashSequence()
    {
        isTransition = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay); //invoke delays the execution of the method given as first parameter by x seconds given in argument as second param
    }

    void ReloadLevel()
    {
        // SceneManager.LoadScene("Sandbox"); //you can either write name of your scene or enter its index like 0,1,2,3,....
        // same as
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevel + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
