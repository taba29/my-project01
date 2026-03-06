using UnityEngine;

public interface IMoveInput
{
    Vector2 Move { get; }   // x=左右, y=前後
    bool Run { get; }       // 走るか
}
