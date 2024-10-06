using System;

public class BirdProjectile : Projectile
{
    public event Action<BirdProjectile> Destroyed;
}
