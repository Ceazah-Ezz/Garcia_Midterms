using JetBrains.Annotations;
using UnityEngine;

public class Green : EnemyBase
{
    protected override Bullet.BulletColorType EnemyColor => Bullet.BulletColorType.Green;

    private void Start()
    {
        speed = 7f;
    }
}