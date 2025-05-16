using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timetoWait = 2f;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] AudioClip CrashSFX;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;
    AudioSource audioSource;
    bool isControllable = true;
    bool isCollidable = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            nextLevel();
        }else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log("CKey was Pressed");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable )
        {
            return;
        }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everthig is looking good!");
                break;
            case "Finish":
                StartnextSeq();
                break;
            default:
                StartcrashSeq();
                break;
        }
    }
    public void StartcrashSeq()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSFX);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", timetoWait);
    }
    public void StartnextSeq()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSFX);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("nextLevel", timetoWait);
    }
    public void reloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void nextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene==SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
