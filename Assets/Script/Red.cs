using JetBrains.Annotations;
using UnityEngine;

public class Red : EnemyBase
{
    protected override Bullet.BulletColorType EnemyColor => Bullet.BulletColorType.Red;

    private void Start()
    {
        speed = 5f;
    }
}
