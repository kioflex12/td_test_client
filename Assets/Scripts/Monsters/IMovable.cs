using UnityEngine;

namespace Monsters
{
    public interface IMovable {
        Transform Transform { get; }
        Transform MoveTarget { get; }
        float MoveSpeed { get; }
        void Move(Transform target);
        Vector3 GetPredictPosition(float predictTime);
    }
}