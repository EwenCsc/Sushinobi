/* 
 * @author Garnier Gael
 * @date 10/10/2018
 * @file AddScore.cs
 */

using UnityEngine;

public class Armor : PowerUp {

    protected override void OnCollect()
    {
        base.OnCollect();

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().AddArmor();
    }
}
