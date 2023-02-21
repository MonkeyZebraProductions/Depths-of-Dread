using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biter_AI_State 
{
    Spawn,
    Wander, // Pick area to wander radomly
    Patrol, // Move Through preplaced Patrol Points
    Chase, //Chase the Player
    Attack,
    Retreat, // Move Away from Player After Attack
    Dead,

}
