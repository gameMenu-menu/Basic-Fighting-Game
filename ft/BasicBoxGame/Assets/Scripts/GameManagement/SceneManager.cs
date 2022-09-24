using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public SceneController controller;
    
    public PoolingManager Pool;
    
    public UIManager uIManager;

    public Joistick joistick;

    public CameraController cameraController;

    public StageController stageController;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        controller.OnPrepareScene += OnPrepareScene;
    }

    void OnDisable()
    {
        controller.OnPrepareScene -= OnPrepareScene;
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        controller.PrepareScene();
        controller.StartScene();
    }

    void OnPrepareScene()
    {
        StopAllCoroutines();
    }
}
