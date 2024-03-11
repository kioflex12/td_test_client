using UnityEngine;

namespace Monsters
{
    public interface IMovable {
        Transform Transform { get; }
        void Move(Transform target);
    }
}