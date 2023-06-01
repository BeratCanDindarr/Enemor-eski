using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{

    // Define a Skill class to hold the properties of each skill
    [System.Serializable]
    public class Skill
    {
        public string name;
        public float power;
        public float cost;
        public float cooldown;
        [HideInInspector] public float lastUsedTime = -9999.0f;
    }

    // Define an array of skills that the player can use
    public Skill[] skills;

    // Define the player's mana and health
    public float maxMana = 100.0f;
    public float maxHealth = 100.0f;
    private float currentMana = 100.0f;
    private float currentHealth = 100.0f;

    // Update the skill cooldowns
    void Update()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].lastUsedTime += Time.deltaTime;
        }
    }

    // Use a skill based on the skill index
    public void UseSkill(int skillIndex)
    {
        if (skillIndex < 0 || skillIndex >= skills.Length)
        {
            Debug.LogError("Invalid skill index!");
            return;
        }
        Skill skill = skills[skillIndex];
        if (!CanUseSkill(skill))
        {
            return;
        }
        currentMana -= skill.cost;
        skill.lastUsedTime = 0.0f;
        ApplySkillEffect(skill);
    }

    // Check if the player can use a skill based on the cost, cooldown, and current time
    private bool CanUseSkill(Skill skill)
    {
        if (currentMana < skill.cost)
        {
            Debug.Log("Not enough mana!");
            return false;
        }
        if (Time.time - skill.lastUsedTime < skill.cooldown)
        {
            Debug.Log("Skill is on cooldown!");
            return false;
        }
        return true;
    }

    // Apply the effect of a skill based on the skill properties
    private void ApplySkillEffect(Skill skill)
    {
        switch (skill.name)
        {
            case "Fireball":
                // Apply the Fireball effect, such as damage to an enemy
                break;
            case "IceBlast":
                // Apply the IceBlast effect, such as freezing an enemy
                break;
            case "Heal":
                currentHealth = Mathf.Min(currentHealth + skill.power, maxHealth);
                // Apply the Heal effect, such as restoring the player's health
                break;
            default:
                Debug.LogError("Invalid skill name!");
                break;
        }
    }
}

