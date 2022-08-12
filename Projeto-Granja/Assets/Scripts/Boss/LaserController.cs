using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public Transform Target;
    public Transform Player;
    public ParticleSystem ImpactParticles;
    public LineRenderer Line;
    [SerializeField] private Animator animator;

    public bool LaserActive = false;

    [Header("Styling")]
    public AnimationCurve AnticipationCurve = new AnimationCurve(new Keyframe(0,0.1f), new Keyframe(1,0.1f));
    public AnimationCurve TransitionCurve = new AnimationCurve(new Keyframe(0,0.1f), new Keyframe(1,0.1f));
    public AnimationCurve LaserCurve = new AnimationCurve(new Keyframe(0,0.5f), new Keyframe(1,0.5f));
    public Gradient AnticipationColor = new Gradient();
    public Gradient TransitionColor = new Gradient();
    public Gradient LaserColor = new Gradient();

    #if UNITY_EDITOR
    #region Testing

    [Header("Testing")]
    public bool TestLaser;
    public float TestDuration;
    public float TestAnticipation;
    
    private void OnValidate()
    { 
        if (TestLaser) 
        { 
            TestLaser = false; 
            ActivateLaser(TestDuration, TestAnticipation);
        }
    }
    #endregion
    #endif

    private void Start()
    {
        LaserActive = false;
        Line.enabled = false;
        ImpactParticles.Stop();
    }

    private void FixedUpdate()
    {
        Target.position = Player.position;
        Line.SetPosition(1, Target.localPosition);
    }

    public void ActivateLaser(float duration, float anticipation)
    {
        StartCoroutine(LaserSequence(duration, anticipation));
    }

    private IEnumerator LaserSequence(float duration, float anticipation)
    {
        LaserActive = true;
        Line.enabled = true;
        Line.widthCurve = AnticipationCurve;
        Line.colorGradient = AnticipationColor;
        animator.SetInteger("Laser", 1);
        
        yield return new WaitForSeconds(anticipation * 0.9f);
        
        Line.widthCurve = TransitionCurve;
        Line.colorGradient = TransitionColor;
        
        yield return new WaitForSeconds(anticipation * 0.1f);
        
        Line.widthCurve = LaserCurve;
        Line.colorGradient = LaserColor;
        ImpactParticles.Play();
        animator.SetInteger("Laser", 2);
        
        yield return new WaitForSeconds(duration);
        
        LaserActive = false;
        Line.enabled = false;
        ImpactParticles.Stop();
        animator.SetInteger("Laser", 0);
    }
}
