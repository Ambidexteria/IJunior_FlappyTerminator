using System;

public class EnemyProjectile : Projectile
{
    public event Action<EnemyProjectile> Destroyed;
}
