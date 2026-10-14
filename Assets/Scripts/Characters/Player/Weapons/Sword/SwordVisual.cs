using UnityEngine;

public class SwordVisual : SwordSlashVisual
{
    public void TriggerEndAttackAnimation()
    {
        sword.AttackColliderTurnOff();
    }
}
