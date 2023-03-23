using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biter_AI_BaseState
{
    public abstract void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller);
    public abstract void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller);
    public abstract void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller);
}
