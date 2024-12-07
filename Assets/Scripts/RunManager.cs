using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RunManager : Subject, IObserver
{
   public static RunManager Instance;
   [SerializeField] private int countdownTime = 3;
   private TextMeshProUGUI countdownTimerUI;
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(this);
      }
   }

   private void Start()
   {
      GameManager.Instance.AddObserver(Instance);
   }


   private void StartRun()
   {
      countdownTimerUI = GameObject.Find("CountDownTimerUI").GetComponent<TextMeshProUGUI>();
      Debug.Log(countdownTimerUI);
      StartCoroutine(CountDown(countdownTime));
   }

   public void EndRun()
   {
      NotifyObserver("EndScene");
   }
   
   private IEnumerator CountDown(int time)
   {
      countdownTimerUI.enabled = true;
      while (time > 0)
      {
         // Show the remaining countdown timer
         Debug.Log(time + "s remaing");
         countdownTimerUI.text = time.ToString();
         yield return new WaitForSeconds(1f);
         time--;
      }
      countdownTimerUI.text = "Go!!!";
      yield return new WaitForSeconds(0.5f);
      countdownTimerUI.enabled = false;
      NotifyObserver("GameScene");
   }

   public void OnNotify(string sceneName)
   {
      switch (sceneName)
      {
         case "MenuScene":
            
            break;
         case "GameScene":
            StartRun();
            break;
         case "EndScene":
            
            break;
         default:
            break;
      }
   }
   

   private void OnDisable()
   {
      GameManager.Instance.RemoveObserver(RunManager.Instance);
   }
}
