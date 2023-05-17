using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mincing : ProcessController
{
    public override void prepIngredients(GameObject go)
    {
        //다지기 조리과정
        Ingredient ig = go.GetComponent<Ingredient>();
        Destroy(ig);//오브젝트 제거
        //오브젝트를 넘겨 받고 오브젝트의 상태/종류에 따라서 손질한다.
        if (ig.Type == Define.IngredientType.Meat)
        {
            //새로 조리된 오브젝트를 생성후 주변에 던진다.
            //문제1 해당 오브젝트가 어떤 조리대인가를 확인 해야함;
            //문제2 지금 조리과정이 생략됨
        }

        base.prepIngredients(go);
    }
}
