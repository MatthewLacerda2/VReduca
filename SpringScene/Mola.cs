using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mola : MonoBehaviour
{
    public GameObject springModel;
    SpringJoint spring;
    public GameObject topBase;
    public GameObject weight;
    float constanteElastica, amortecimento;
    float elasticity;


    void Awake()
    {
        spring = topBase.GetComponent<SpringJoint>();
        topBase.GetComponent<Transform>();
        weight.GetComponent<Transform>();
    }

    private void Update()
    {
        springModel.transform.localScale = new Vector3(1,1,(topBase.transform.position.y - weight.transform.position.y + .5f)* 0.424033f);
        Debug.Log(springModel.transform.localScale);
    }

    public void setConstanteElastica(float value)
    {
        float valueFinal = spring.spring;
        valueFinal += value;
        if (valueFinal < 0)
        {
            valueFinal = 0;
        }
        else if (valueFinal > 50)
        {
            valueFinal = 50;
        }

        spring.spring = valueFinal;

    }


    public float getConstanteElastica()
    {
        constanteElastica = spring.spring;
        return constanteElastica;

    }

    public void setAmortecimento(float value)
    {
        float valueFinal = spring.damper;
        valueFinal += value;
        if (valueFinal < 0)
        {
            valueFinal = 0;
        }
        else if (valueFinal > 1)
        {
            valueFinal = 1;
        }


        spring.damper = valueFinal;

    }


    public float getAmortecimento()
    {
        amortecimento = spring.damper;
        return amortecimento;

    }






}
