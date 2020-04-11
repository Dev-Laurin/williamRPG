using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayer
{
    float[] Position = new float[3]; 
    int PartyPosition; 
    string BattleIdleAnimation; 
    string Name;  
    int HP; 
    int MaxHP; 
    int MaxSP; 
    int SP; 
    int Level; 
    int Defense; 
    int Strength; 
    int Speed; 

    string WalkLeftAnim; 
    string WalkRightAnim; 
    string WalkUpAnim; 
    string WalkDownAnim; 

    // Start is called before the first frame update
    public void Save(PlayableUnit pu)
    {
        // Position[0] = pu.transform.position.x;
        // Position[1] = pu.transform.position.y;
        // Position[2] = pu.transform.position.z;
        // PartyPosition = pu.partyPos; 
        // BattleIdleAnimation = pu.battleIdle; 
        // Name = pu.name; 
        // HP = pu.hp; 
        // MaxHP = pu.maxHP; 
        // SP = pu.sp; 
        // MaxSP = pu.maxSP; 
        // Level = pu.level; 
        // Defense = pu.defense; 
        // Strength = pu.strength;
        // Speed = pu.speed; 
        
        // WalkLeftAnim = pu.walkLeft; 
        // WalkRightAnim = pu.walkRight; 
        // WalkUpAnim = pu.walkUp; 
        // WalkDownAnim = pu.walkDown; 

    }

    // Update is called once per frame
    void LoadPlayer()
    {
        
    }
}
