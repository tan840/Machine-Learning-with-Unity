using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain : MonoBehaviour
{
    public int DnaLength = 1;
    public float timeAlive;
    public DNA_Ethan dna;

    private ThirdPersonCharacter m_character;
    private Vector3 m_move;
    private bool m_jump;
    bool alive = true;
    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }
    public void Init()
    {
        dna = new DNA_Ethan(DnaLength, 6);
        m_character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }


    private void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        bool crouch = false;
        if (dna.GetGene(0)==0)
        {
            v = 1;
        }
        else if (dna.GetGene(0)==1)
        {
            v = -1;
        }
        else if (dna.GetGene(0) == 2)
        {
            h = -1;
        }
        else if (dna.GetGene(0) == 3)
        {
            h = 1;
        }
        else if (dna.GetGene(0) == 4)
        {
            m_jump = true;
        }
        else if (dna.GetGene(0) == 5)
        {
            crouch = true;
        }

        m_move = v * Vector3.forward + h * Vector3.right;
        m_character.Move(m_move,crouch,m_jump);
        m_jump = false;
        if(alive)
        {
            timeAlive += Time.deltaTime;
        }
    }


}
