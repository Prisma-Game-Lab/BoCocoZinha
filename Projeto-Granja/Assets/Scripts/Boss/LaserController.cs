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
    [SerializeField] private BossController bc;

    public bool _LaserActive = false;
    public bool LaserActive {
        get => _LaserActive;
        set{
            Target.gameObject.SetActive(value);
            _LaserActive = value;
        }
    }
    private bool searching = true;
    private Vector3 finalPos;

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
    }

    private void FixedUpdate()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if(searching)
        {
            Target.position = Player.position;
        }
        else
        {
            Target.position = finalPos;
        }

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
        searching = true;
        Line.widthCurve = AnticipationCurve;
        Line.colorGradient = AnticipationColor;
        animator.SetInteger("Laser", 1);
        
        yield return new WaitForSeconds(anticipation * 0.9f);

        Line.widthCurve = TransitionCurve;
        Line.colorGradient = TransitionColor;
        
        yield return new WaitForSeconds(anticipation * 0.1f);

        AudioManager.instance.PlayForSeconds("boss_laser", duration);
        Line.widthCurve = LaserCurve;
        Line.colorGradient = LaserColor;
        ImpactParticles.Play();
        finalPos = Player.position;
        searching = false;
        animator.SetInteger("Laser", 2);
        
        yield return new WaitForSeconds(duration);
        
        LaserActive = false;
        Line.enabled = false;
        ImpactParticles.Stop();
        animator.SetInteger("Laser", 0);
        StartCoroutine(bc.AttackCooldown());
    }
}
