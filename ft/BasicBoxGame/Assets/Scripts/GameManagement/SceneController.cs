using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneState {None, Prepared, Started, Ended}
public class SceneController : MonoBehaviour
{
    public delegate void OnPrepareSceneDelegate();
    public event OnPrepareSceneDelegate OnPrepareScene;
    
    public delegate void OnStartSceneDelegate();
    public event OnStartSceneDelegate OnStartScene;

    public delegate void OnLevelVictoryDelegate();
    public event OnLevelVictoryDelegate OnLevelVictory;
    
    public delegate void OnLevelFailDelegate();
    public event OnLevelFailDelegate OnLevelFail;

    public delegate void OnClickContinueDelegate();
    public event OnClickContinueDelegate OnClickContinue;

    public delegate void OnKillEnemyDelegate();
    public event OnKillEnemyDelegate OnKillEnemy;


    SceneState CurrentState;
    


    public void PrepareScene()
    {
        if(GetCurrentState() != SceneState.None)
        {
            Debug.LogError("STATE ERROR");
            return;
        }
        OnPrepareScene?.Invoke();
        ChangeState(SceneState.Prepared);
    }

    public void StartScene()
    {
        if(GetCurrentState() != SceneState.Prepared)
        {
            Debug.LogError("STATE ERROR");
            return;
        }
        OnStartScene?.Invoke();
        ChangeState(SceneState.Started);
    }

    public void LevelVictory()
    {
        OnLevelVictory?.Invoke();
        ChangeState(SceneState.Ended);
    }

    public void LevelFail()
    {
        OnLevelFail?.Invoke();
        ChangeState(SceneState.Ended);
    }

    public void ClickContinue()
    {
        
        if(GetCurrentState() != SceneState.Ended)
        {
            Debug.LogError("STATE ERROR");
            return;
        }

        OnClickContinue?.Invoke();

        ChangeState(SceneState.None);

        PrepareScene();

        StartScene();
        
    }

    public void ChangeState(SceneState state)
    {
        CurrentState = state;
    }

    public SceneState GetCurrentState()
    {
        return CurrentState;
    }

    public void KillEnemy()
    {
        OnKillEnemy?.Invoke();
    }
    
}
