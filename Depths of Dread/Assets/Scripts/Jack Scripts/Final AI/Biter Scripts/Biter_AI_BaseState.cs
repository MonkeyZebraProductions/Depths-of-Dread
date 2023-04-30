using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biter_AI_BaseState
{
    public abstract void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius);
    public abstract void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius);
    public abstract void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius);
}
