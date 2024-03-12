using Monsters;

namespace Towers
{
    public interface IShootable { 
        void TryShoot(IDamageable target);
    }
}