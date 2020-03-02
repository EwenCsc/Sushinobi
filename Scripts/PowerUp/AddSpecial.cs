using UnityEngine;

public class AddSpecial : PowerUp
{
    protected override void OnCollect()
    {
        base.OnCollect();

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().AddUlti(25);
    }
}
