using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiAnimationController : MonoBehaviour
{
    public float timeBetweenAnimationStarts;
    public List<Animator> animators;
    private static readonly int GrowKey = Animator.StringToHash("Grow");

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPlayerWin.AddListener(StartAnimationRoutine);        
    }

    private void StartAnimationRoutine()
    {
        StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        var animationList = new List<Animator>(animators);

        while (animationList.Count > 0)
        {
            var index = Random.Range(0, animationList.Count);
            var animator = animationList[index];
            animationList.RemoveAt(index);
            animator.SetTrigger(GrowKey);
            yield return new WaitForSeconds(timeBetweenAnimationStarts);
        }
    }
}
