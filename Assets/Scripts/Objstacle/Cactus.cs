using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Obstacle
{
    protected override void OnTrigger(CharacterBase player)
    {
        if (player == null) return;

        IHealth health = player as IHealth;

        if (health != null)
        {
            health.OnHit();
        }
        
        if(player is Character_HpGen hgPlayer && player.CheckIsFever())
        {
            GetComponentInParent<Rigidbody2D>().AddForce((transform.parent.position - player.transform.position + Vector3.up) * 10f, ForceMode2D.Impulse);
            hgPlayer.OnCactusHit();

            StopAllCoroutines();
            StartCoroutine(DisableRoutine());
        }
    }

    /// <summary>
    /// 비활성화 코루틴 (날라갈 때 실행)
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableRoutine()
    {
        float timeElpased = 0f;

        while(timeElpased < 2f)
        {
            timeElpased += Time.deltaTime;
            yield return null;
        }

        GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;    // 가속도 제거
        transform.parent.gameObject.SetActive(false);
    }
}