using UnityEngine;

public class RandomMoveInput : MonoBehaviour, IMoveInput
{
    public float changeInterval = 1.0f;
    public float runChance = 0.3f;
    public bool avoidStop = true;

    public Vector2 Move { get; private set; }
    public bool Run { get; private set; }

    float nextChangeTime;

    void OnEnable()
    {
        nextChangeTime = 0f;
    }

    void Update()
    {
        if (Time.time >= nextChangeTime)
        {
            Vector2 v = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)); // -1/0/1
            if (avoidStop && v.sqrMagnitude < 0.01f) v = Vector2.up;

            Move = Vector2.ClampMagnitude(v, 1f);
            Run = (Random.value < runChance);

            nextChangeTime = Time.time + changeInterval;
        }
    }
}
