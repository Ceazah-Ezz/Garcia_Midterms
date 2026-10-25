using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected abstract Bullet.BulletColorType EnemyColor { get; }

    [Header("Base info")]
    [SerializeField] protected float speed = 5f;
    private Transform playerLocation;

    [Header("Other things idk")]
    [SerializeField] private string player = "Player";
    //[SerializeField] private string bulletTag = "Bullet";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        GameObject playerTarget = GameObject.FindGameObjectWithTag(player);

        if (playerTarget != null) // Moves towards player
        {
             playerLocation = playerTarget.transform;
            Debug.Log("Found you!");
        }
        else
        {
            Debug.Log("Player not found...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLocation != null)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, step);

            Vector3 direction = playerLocation.position - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }

    private void OnTriggerEnter(Collider BulletCol)
    {
        Bullet bullet = BulletCol.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            // Compare the bullet's color to THIS enemy's color
            if (bullet.currentBulletColor == EnemyColor)
            {
                Debug.Log($"{gameObject.name} DIES.");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"{gameObject.name} gets hit by the wrong color...");
            }
        }
    }
}
