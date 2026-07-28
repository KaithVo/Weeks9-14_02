using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Animator heroAnimator;
    public bool isMoving = false;
    public AudioSource footstepAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        heroAnimator.SetBool("isMoving", isMoving);
    }
    public void OnFootstep()
    {
        footstepAudioSource.Play();
    }
}
