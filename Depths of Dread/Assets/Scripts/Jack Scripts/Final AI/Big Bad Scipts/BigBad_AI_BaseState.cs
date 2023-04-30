using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BigBad_AI_BaseState
{
    public abstract void EnterState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius);

    public abstract void UpdateState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius);

    public abstract void ExitState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius);


}
