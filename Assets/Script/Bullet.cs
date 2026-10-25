using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string Enemy = "Enemy";
    private Transform target;

    [Header("Bullet attributes")]
    public float speed = 50f;

    // Define the matching enum types here
    public enum BulletColorType
    {
        Red,
        Green,
        Blue
    }
    
    // Tracks what color this specific bullet is
    public BulletColorType currentBulletColor;

    // UPDATED: Now accepts both the target and the color from the turret
    public void Pewpew(Transform _target, BulletColorType turretColor)
    {
        target = _target;
        currentBulletColor = turretColor;

        SetMaterialColor();
    }

    private void SetMaterialColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        switch (currentBulletColor)
        {
            case BulletColorType.Red:
                renderer.material.color = Color.red;
                break;
            case BulletColorType.Green:
                renderer.material.color = Color.green;
                break;
            case BulletColorType.Blue:
                renderer.material.color = Color.blue;
                break;
        }
    }

    #region [Bullet code]
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            // You can trigger your custom hit/damage logic here based on 'currentBulletColor'
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    #endregion

    void OnTriggerEnter(Collider EnemyCol)
    {
        if (EnemyCol.gameObject.CompareTag(Enemy))
        {
            Destroy(gameObject);
        }
    }
}