using UnityEngine;

public class CameraFollowMoveDir : MonoBehaviour
{
    public Transform player;      // Player（Root）
    public float rotateSpeed = 10f;

    void LateUpdate()   // ← UpdateじゃなくLateUpdateが大事
    {
        if (player == null) return;

        Quaternion targetRot = Quaternion.Euler(
            0f,
            player.eulerAngles.y,
            0f
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }
}
