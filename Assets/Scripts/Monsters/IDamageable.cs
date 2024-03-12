namespace Monsters
{
    public interface IDamageable
    {
        bool IsAlive { get; }
        void ApplyDamage(float damage);
        void Kill();
    }
}