using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaloScript : MonoBehaviour
{
    
    public Animator animator;
    public EnemyScript EnemyS;
    public LineRenderer Lazer;

    public bool Cooldown;

    public GameObject Eye;
    RaycastHit hit;

    public Transform HeadBone;

    
    void Update()
    {
        //Leg Attack
        if(EnemyS.inPlayerRange) 
            animator.SetBool("LegAttack", true);

        //Moving Animation difference
        if(EnemyS.CC.velocity.sqrMagnitude > 0)
            animator.SetBool("StartMoving", true);
        else {
            animator.SetBool("isMoving", false);
            animator.SetBool("StartMoving", false);    }

        // lazer attack
        if(!Cooldown && EnemyS.distance > EnemyS.maxDistance && !animator.GetBool("LazerAttack") ) {
            animator.SetBool("LazerAttack", true);
            EnemyS.AllowMovement = false;
        }

        if(animator.GetBool("LazerAttack")) {
        Vector3 difference = EnemyS.player.position - EnemyS.transform.position;
        float angle = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg + 90;
        HeadBone.eulerAngles = Vector3.down * (angle - 160);   }

        if(!animator.GetBool("LazerAttack") && (HeadBone.localEulerAngles.y < -3.1f || HeadBone.localEulerAngles.y > 3.1f)) {
            HeadBone.eulerAngles += new Vector3(0, 3f * (HeadBone.localEulerAngles.y / Mathf.Abs(HeadBone.localEulerAngles.y)), 0);
        }
    }

    public void StopLegAttack() {
        animator.SetBool("LegAttack", false);
    }

    public void ContinueMovement() {
        if(EnemyS.CC.velocity.sqrMagnitude > 0)
            animator.SetBool("isMoving", true);
    }

    public IEnumerator LazerAttackCooldown() {
        Cooldown = true;
        yield return new WaitForSeconds(20f);
        Cooldown = false;
    }


    public void LazerBeam() {
        StartCoroutine(LazerShoot(0, 40));
    }

    IEnumerator LazerShoot(float time, float distance) {
        //checks hit distance
        if(Physics.Raycast(Eye.transform.position, Eye.transform.TransformDirection(-Vector3.forward), out hit, 40)) { // check here <----
            distance = hit.distance;
        }else
            distance = 40f;

        //
        time += 2;
        yield return new WaitForFixedUpdate();
        Lazer.SetPosition(1, new Vector3(0, 0, time));
        if (time < distance)
            StartCoroutine(LazerShoot(time, distance));
        else    {
            StartCoroutine(LazerCooldown(distance));
            EnemyS.HitPlayer(new Vector2(13, 35));    }   //left off <----
    }

    public void EndLazerAttack()
    {
        animator.SetBool("LazerAttack", false);
        EnemyS.AllowMovement = true;   
        Lazer.SetPosition(1, new Vector3(0, 0, 0));
    }
    
    IEnumerator LazerBack(float time) {
        time -= 1;
        yield return new WaitForFixedUpdate();
        Lazer.SetPosition(1, new Vector3(0, 0, time));
        if (time > 0)
            StartCoroutine(LazerBack(time));
    }

    IEnumerator LazerCooldown(float distance) {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(LazerBack(distance));
    }

}
