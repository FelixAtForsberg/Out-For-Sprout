using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioPlayType audioType;

    private void OnTriggerEnter2D(Collider2D col)
    {
        AudioHandler.Instance.Play(audioType);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        AudioHandler.Instance.Play(audioType);
    }
}
