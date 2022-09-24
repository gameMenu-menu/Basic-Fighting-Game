using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public Transform Ground;

    public int StageNumber;

    public GameVariables Variables;

    int EnemyCount;

    public HumanController PlayerController;

    void OnEnable()
    {
        SceneManager.Instance.controller.OnPrepareScene += OnPrepareScene;
        SceneManager.Instance.controller.OnLevelVictory += OnLevelVictory;
        SceneManager.Instance.controller.OnKillEnemy += OnKillEnemy;
    }

    void OnDisable()
    {
        SceneManager.Instance.controller.OnPrepareScene -= OnPrepareScene;
        SceneManager.Instance.controller.OnLevelVictory -= OnLevelVictory;
        SceneManager.Instance.controller.OnKillEnemy -= OnKillEnemy;
    }

    void OnPrepareScene()
    {
        PlayerController.Reset(Variables.BasePlayerHealth, Variables.BasePlayerDamage);
        SceneManager.Instance.Pool.CleanScene();
        SpawnEnemies();
    }

    void OnLevelVictory()
    {
        StageNumber++;
    }

    void SpawnEnemies()
    {
        EnemyCount = 0;
        Vector3 size = Ground.GetComponent<MeshRenderer>().bounds.size;

        float GroundX = (size/2f).x;

        float x1 = Ground.position.x - GroundX;
        float x2 = Ground.position.x + GroundX;

        float GroundZ = (size/2f).z;

        float z1 = Ground.position.z - GroundZ;
        float z2 = Ground.position.z + GroundZ;

        float GroundY = Ground.position.y;

        int count = Variables.BaseEnemyQuantity + Variables.EnemyIncreasementWithStage * StageNumber;
        

        for(int k=0; k<count; k++)
        {
            GameObject enemy = SceneManager.Instance.Pool.ObtainFromPool(ObjectType.Enemy);
            HumanController controller = enemy.GetComponent<HumanController>();
            enemy.transform.position = new Vector3(Random.Range(x1, x2), GroundY, Random.Range(z1, z2));
            
            
            enemy.SetActive(true);
            controller.Reset(Variables.BaseEnemyHealth, Variables.BaseEnemyDamage);
            
            controller.character.Agent.enabled = true;

            EnemyCount++;
            
        }
    }

    void OnKillEnemy()
    {
        EnemyCount--;

        if(EnemyCount <= 0)
        {
            SceneManager.Instance.controller.LevelVictory();
        }
    }
}
