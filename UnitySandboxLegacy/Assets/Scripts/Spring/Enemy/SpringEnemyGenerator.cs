using System.Collections.Generic;
using UnityEngine;

public class SpringEnemyGenerator : MonoBehaviour {
    void Start() {
        m_enemies = new List<SpringEnemy>(m_numEnemies);
        for (int i=0;i< m_numEnemies;++i) {
            const float DELAY = 0.1f;
            SpringEnemy enemy = CreateEnemy();
            enemy.SetNextTargetPos(m_mainTarget.position, i * DELAY);
            m_enemies.Add(enemy);
        }        
        
        InvokeRepeating(nameof(RandomizeMainTargetPos), 2.0f, 3.0f);
    }
    
//----------------------------------------------------------------------------------------------------------------------    

    SpringEnemy CreateEnemy() {
        GameObject enemyGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Transform  t       = enemyGo.transform;
        t.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        t.position = new Vector3(0, 0, 0);
        t.SetParent(m_enemyParent);
                        
        //add behaviour
        SpringEnemy enemy = enemyGo.AddComponent<SpringEnemy>();
        enemy.SetCoefficients(m_enemySpringCoef, m_enemyDragCoef);
        
        return enemy;
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    
    void RandomizeMainTargetPos() {

        const float HALF_AREA_SIZE = 3.0f;
        Vector3 pos = new Vector3(Random.Range(-HALF_AREA_SIZE, HALF_AREA_SIZE), Random.Range(-HALF_AREA_SIZE, HALF_AREA_SIZE),0);
        m_mainTarget.position = pos;

        int length = m_enemies.Count;
        for (int i = 0; i < length; ++i) {
            const float DELAY = 0.1f;
            m_enemies[i].SetNextTargetPos(pos, i * DELAY);
        }

    }        

//----------------------------------------------------------------------------------------------------------------------
    [SerializeField] private Transform m_mainTarget = null;
    [SerializeField] private Transform m_enemyParent = null;
    [SerializeField] private int       m_numEnemies = 10;

    [SerializeField] private float m_enemySpringCoef = 4f;
    [SerializeField] private float m_enemyDragCoef = 1f;


    private List<SpringEnemy> m_enemies;
}
