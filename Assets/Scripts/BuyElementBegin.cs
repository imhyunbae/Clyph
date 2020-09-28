using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyElementBegin : MonoBehaviour
{
    public Vector3 YContractionScale;
    public Vector3 XContractionScale;
    public Vector3 NormalScale;

    public Vector3 Target;

    Action OnFinish; 

    
    public void Init(Vector3 _StartPos, Vector3 _EndPos, Action _OnFinish)
    {
        transform.localScale = NormalScale;
        transform.position = _StartPos;
        OnFinish = _OnFinish;
        Target = _EndPos;
        StartCoroutine(Directing());
    }
    IEnumerator Directing()
    {

        float  Timer = 0f;
        while (Timer <= 0.1f)
        {
            Timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, YContractionScale, Timer / 0.4f);
            yield return null;
        }

        Timer = 0f;
        while (Timer <= 0.1f)
        {
            Timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, XContractionScale, Timer / 0.4f);
            yield return null;
        }

        Timer = 0f;
        while (Timer <= 0.2f)
        {
            Timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, NormalScale, Timer / 0.2f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5F);

        Timer = 0f;
        while (Timer <= 0.5f)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, Target, Timer / 0.5f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Timer / 0.5f);
            yield return null;
        }
        OnFinish();
        GameObject.Destroy(gameObject);
    }

}
