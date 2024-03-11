namespace Monsters
{
    public interface IDamageable
    {
        void ApplyDamage(float damage);
        void Kill();
    }
}