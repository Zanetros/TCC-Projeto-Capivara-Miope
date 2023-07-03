using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager instance;	
    
    void Awake()
    {
        instance = this;
    }

    [SerializeField] private ScreenTint screenTint;
    [SerializeField] private CameraConfiner cameraConfiner;
    string currentScene;
    private AsyncOperation unload;
    private AsyncOperation load;

    public PlayerMovementTeste playerMovementTeste;
    public CharacterInteractController characterInteractController;
    public InventoryController inventoryController;
    public Animator playerAnimator;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        playerMovementTeste = FindObjectOfType<PlayerMovementTeste>();
        characterInteractController = FindObjectOfType<CharacterInteractController>();
        inventoryController = FindObjectOfType<InventoryController>();
        playerAnimator = FindObjectOfType<Animator>();
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        playerMovementTeste.enabled = false;
        characterInteractController.enabled = false;
        inventoryController.enabled = false;
        playerAnimator.enabled = false;
        StartCoroutine(Transition(to, targetPosition));
    }
    
    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1F / screenTint.speed + 0.1F);
        
        SwitchScene(to, targetPosition);

        while (load != null && unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }
            yield return new WaitForSeconds(0.1F);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTint();

    }
    
    public void SwitchScene(string to, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;

        Transform playerTransform = GameManager.instance.player.transform;
        
        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
            playerTransform,
            targetPosition - playerTransform.position
        );
        playerMovementTeste.enabled = true;
        characterInteractController.enabled = true;
        inventoryController.enabled = true;
        playerAnimator.enabled = true;
        GameManager.instance.player.transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
    }

}