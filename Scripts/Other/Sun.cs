using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
enum Sunstate
{
    Enable,
    Disable
}
public class Sun : MonoBehaviour
{
    private Sunstate sunstate = Sunstate.Enable;
    public float moveDuration = 1;
    public int point = 50;

    private void Start()
    {
        sunstate = Sunstate.Enable;
        Destroy(gameObject, 10);
    }
    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration);
    }
    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1;
        Vector3 cenTerPos = (transform.position + targetPos) / 2;
        float distance=Vector3.Distance(transform.position, targetPos);
        cenTerPos.y += (distance / 2);
        transform.DOPath(new Vector3[] { transform.position, cenTerPos, targetPos }, moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    public void OnMouseDown()
    {        
        if (sunstate == Sunstate.Disable) return;
        AudioManager.Instance.Play("points");
        transform.DOMove(SunManager.Instance.GetSunPointTextPositin(), moveDuration).OnComplete(
            () =>
            {
                Destroy(this.gameObject);
                SunManager.Instance.AddSun(point);
            });
    }

    public void SunDis()
    {
        sunstate = Sunstate.Disable;        
    }
}
