using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Delegado y evento para la destrucci�n de enemigos
    public delegate void EnemyDestroyed(EnemyType enemyType);
    public event EnemyDestroyed OnEnemyDestroyed;

    // M�todo para manejar la eliminaci�n de enemigos
    public void EnemyEliminated(EnemyType enemyType)
    {
        OnEnemyDestroyed?.Invoke(enemyType);

        Debug.Log("Enemy destroyed. Type: " + enemyType); // Agrega este mensaje de depuraci�n
    }
}