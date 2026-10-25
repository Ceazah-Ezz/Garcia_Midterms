using UnityEngine;
using UnityEngine.InputSystem;
using static Bullet;
using static ColorTurret;

public class ColorTurret : MonoBehaviour
{
    [Header("Tower rotation")]
    private Transform target; //enemy target
    public Transform Rotate;
    public float turnSpd = 10f;
    public string enemyTag = "Enemy";

    [Header("Tower attributes")]
    public MeshRenderer SelectionColor; //Indicates the player if the tower is cliked on or not
    public GameObject Prefabbullet; //Bullet GameObject/prefab
    public Transform firingpoint;
    public float range = 13f;

    [Header("Other stuff")]
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject Instructions;
    [SerializeField] private StateSwitch towerSwitch;


    #region [Find Target]
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (enemyDistance < shortestDistance)
            {
                shortestDistance = enemyDistance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            Attack();
        }
    }
    #endregion

    private void OnMouseDown()
    {
        Debug.Log("Tower clicked!");

        if (towerSwitch != null)
        {
            towerSwitch.Switch();
            UpdateTurretVisualColor(); // Update the mesh color instantly when clicked!
        }
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    #region [Update function]
    void Update()
    {
        #region [Targeting and Rotation]
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookrotate = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotate.rotation, lookrotate, Time.deltaTime * turnSpd).eulerAngles;
        Rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        #endregion
    }
    #endregion

    #region [Color update]
    private void UpdateTurretVisualColor()
    {
        if (SelectionColor == null || towerSwitch == null) return;
        Material towerMaterial = SelectionColor.material;

        switch (towerSwitch.CurrentBulletType)
        {
            case StateSwitch.ColorState.Red:
                towerMaterial.color = Color.red;
                // Double protection for newer Unity Shaders:
                towerMaterial.SetColor("_BaseColor", Color.red);
                break;

            case StateSwitch.ColorState.Green:
                towerMaterial.color = Color.green;
                towerMaterial.SetColor("_BaseColor", Color.green);
                break;

            case StateSwitch.ColorState.Blue:
                towerMaterial.color = Color.blue;
                towerMaterial.SetColor("_BaseColor", Color.blue);
                break;
        }
    }
    #endregion

    void Attack()
    {
        GameObject Objectbullet = (GameObject)Instantiate(Prefabbullet, firingpoint.position, firingpoint.rotation);
        Bullet bullet = Objectbullet.GetComponent<Bullet>();

        if (bullet != null)
        {
            StateSwitch.ColorState currentType = towerSwitch.CurrentBulletType;

            Bullet.BulletColorType bulletColor = (Bullet.BulletColorType)(int)currentType;

            bullet.Pewpew(target, bulletColor);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //Ends game once enemy touches tower
    private void OnTriggerEnter(Collider EnemyCol)
    {
        if (EnemyCol.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("Ow!");
            GameOverUI.SetActive(true);
            Instructions.SetActive(false);

            Time.timeScale = 0;
        }
    }
}
