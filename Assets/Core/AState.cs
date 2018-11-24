using System;
using UnityEngine;
using UnityEngine.Analytics;

public abstract class AState : MonoBehaviour
{
   [NonSerialized]
   public GameManager Manager;

   public abstract void Init();
   public abstract void Enter();
   public abstract void Exit();
   public abstract void Tick();

   public abstract string GetName();


}
