using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    #pragma warning disable 0649
    [SerializeField] private int maxHealth;
    #pragma warning restore 0649

    private int currentHealth;
    private InputController inputController;
    public static bool hasActiveClone;
    public static event System.Action OnPlayerDeath = delegate { };

    private void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    private void Update()
    {
        if (hasActiveClone)
        {
            inputController.enabled = false;
        }
        else
        {
            inputController.enabled = true;
        }
    }

    public void Die()
    {
        OnPlayerDeath();
    }
}
