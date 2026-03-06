using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour, IMoveInput
{
    public Vector2 Move { get; private set; }
    public bool Run { get; private set; }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move = Vector2.ClampMagnitude(new Vector2(h, v), 1f);
        
        // 両Shift対応
        Run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    }
}
