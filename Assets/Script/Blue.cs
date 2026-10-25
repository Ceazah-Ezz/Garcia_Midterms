using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Blue : EnemyBase
{
    protected override Bullet.BulletColorType EnemyColor => Bullet.BulletColorType.Blue;

    private void Start()
    {
        speed = 2f;
    }
}
