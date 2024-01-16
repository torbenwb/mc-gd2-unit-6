using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Coins_SO : ScriptableObject
{
    public int amount = 0;

    public void Reset() => amount = 0;
    public void Add() => amount++;
    public void Subtract() => amount--;
}
