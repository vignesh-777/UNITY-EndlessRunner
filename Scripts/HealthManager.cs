using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public float HealthAmount = 100f;

    

    void Update()
    {
        
        
        if(HealthAmount <= 0){
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
    public void TakeDamage(float damage){
        HealthAmount -= damage;
        
        HealthAmount = Mathf.Clamp(HealthAmount,0,100);
        healthBar.fillAmount = HealthAmount / 100f;
        

    }
    public void Heal(float Healing){
        HealthAmount += Healing;
        HealthAmount = Mathf.Clamp(HealthAmount,0,100);
        healthBar.fillAmount = HealthAmount / 100f;
    }
     
}
