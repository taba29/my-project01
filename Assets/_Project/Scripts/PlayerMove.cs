using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float turnSpeed = 720f;

    public Animator anim;
    public string speedParam = "speed";

    // どの入力を使うか（Inspectorで差し替え可）
    public MonoBehaviour inputSource; // IMoveInput を実装したコンポーネントを入れる

    IMoveInput input;

    void Reset()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Awake()
    {
        if (anim == null) anim = GetComponentInChildren<Animator>();
        input = inputSource as IMoveInput;
    }

    void OnValidate()
    {
        input = inputSource as IMoveInput;
    }

    void Update()
    {
        if (input == null)
        {
            // 何も設定されてないときは止める
            if (anim) anim.SetFloat(speedParam, 0f);
            return;
        }

        Vector2 mv = input.Move;
        bool isRun = input.Run;

        Vector3 in3 = new Vector3(mv.x, 0f, mv.y);
        in3 = Vector3.ClampMagnitude(in3, 1f);

        float spd = moveSpeed * (isRun ? runMultiplier : 1f);

        // 入力がない
        if (in3.sqrMagnitude < 0.0001f)
        {
            if (anim) anim.SetFloat(speedParam, 0f);
            return;
        }

        // カメラ基準に変換（水平のみ）
        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1f, 0f, 1f)).normalized;
        Vector3 camRight   = Vector3.Scale(cam.right,   new Vector3(1f, 0f, 1f)).normalized;

        Vector3 moveDir = (camForward * in3.z + camRight * in3.x);
        moveDir.y = 0f;

        if (moveDir.sqrMagnitude < 0.0001f)
        {
            if (anim) anim.SetFloat(speedParam, 0f);
            return;
        }

        moveDir.Normalize();

        // 移動
        transform.position += moveDir * spd * Time.deltaTime;

        // ★後退中は回転しない（Sブルブル対策）
        bool isBackward = (mv.y < -0.1f);
        if (!isBackward)
        {
            Quaternion rot = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        if (anim)
        {
            float mag = in3.magnitude * (isRun ? 2f : 1f);
            anim.SetFloat(speedParam, mag);
        }
    }
}
