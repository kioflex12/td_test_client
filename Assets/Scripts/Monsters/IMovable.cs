using UnityEngine;

namespace Monsters
{
    public interface IMovable {
        Transform Transform { get; }
        Transform MoveTargetTransform { get; }
        float MoveSpeed { get; }
        void Move(Transform target);
    }
}