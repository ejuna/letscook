using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoastController : ProcessController
{
    public override void prepIngredients(GameObject go)
    {
        Debug.Log("굽기");
        //굽기 조리
        Ingredient ig = go.GetComponent<Ingredient>();

        //오브젝트를 넘겨 받고 오브젝트의 상태/종류에 따라서 손질한다.
        if (ig.Type == Define.IngredientType.Meat)
        {
            Debug.Log("이건 고기류");

            //Destroy(go);//오브젝트 제거
            //새로 조리된 오브젝트를 생성후 주변에 던진다.
            throwObject(go);
            //문제2 지금 조리과정이 생략됨
        }

        base.prepIngredients(go);
    }
}
