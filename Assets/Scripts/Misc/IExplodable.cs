namespace MD.Diggable.Projectile
{
    public interface IExplodable
    {
        void ProcessExplosion(float gemDropPercentage, float stunTime, int bombType);
    }
}