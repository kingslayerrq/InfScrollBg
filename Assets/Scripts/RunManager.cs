using System;
using System.Collections;
using UnityEngine;

public class RunManager : Subject, IObserver
{
   public static RunManager Instance;
   [SerializeField] private int countdownTime = 3;

   private void Awake()
   {
      if (Instance == null)
      {
         DontDestroyOnLoad(this);
      }
   }

 

   private void StartRun()
   {
      StartCoroutine(CountDown(countdownTime));
   }
   
   private IEnumerator CountDown(int time)
   {
      for (int i = time; i > 0; i--)
      {
         // Show the remaining countdown timer
         Debug.Log(i + "s remaing");
         yield return new WaitForSeconds(1f);
      }
      Debug.Log("Go!!!");
      yield return null;
      NotifyObserver();
   }

   public void OnNotify()
   {
      // Start the run when notified by GameManager
      StartRun();
   }

   private void OnEnable()
   {
      GameManager.Instance.AddObserver(this);
   }

   private void OnDisable()
   {
      GameManager.Instance.RemoveObserver(this);
   }
}
