using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUi : MonoBehaviour
{

   [SerializeField]
   private UnityEngine.UI.Image HealthBarForegroundImage;


   public void UpdateHealthBar(HealthController healthController)
   {
    HealthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage;
    


   }
}
