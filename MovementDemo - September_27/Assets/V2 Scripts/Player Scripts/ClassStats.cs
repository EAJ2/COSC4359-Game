using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStats : MonoBehaviour
{
    [SerializeField] private string Class;
    [SerializeField] private float Intelligence;
    [SerializeField] private float Strength;
    [SerializeField] private float Dexterity;
    [SerializeField] private float Sneaky;

    public void SetClass(string val)
    {
        Class = val;
    }

    public string GetClass()
    {
        return Class;
    }

    public void SetIntelligence(float val)
    {
        Intelligence = val;
    }

    public void SetStrength(float val)
    {
        Strength = val;
    }

    public void SetDexterity(float val)
    {
        Dexterity = val;
    }

    public void SetSneaky(float val)
    {
        Sneaky = val;
    }

    public void AddIntelligence(float val)
    {
        Intelligence += val;
    }

    public void AddStrength(float val)
    {
        Strength += val;
    }

    public void AddDexterity(float val)
    {
        Dexterity += val;
    }

    public void AddSneaky(float val)
    {
        Sneaky += val;
    }

    public float GetIntelligence()
    {
        return Intelligence;
    }

    public float GetStrength()
    {
        return Strength;
    }

    public float GetDexterity()
    {
        return Dexterity;
    }

    public float GetSneaky()
    {
        return Sneaky;
    }
}
