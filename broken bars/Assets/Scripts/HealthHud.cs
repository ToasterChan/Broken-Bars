using UnityEngine;

public class HealthHud : MonoBehaviour
{
    [SerializeField] private float startinghealth;
    private float currenthealth;

    private void Awake()
    {
        currenthealth = startinghealth;
    }
    private void TakeDamage(float _damage)
    {

        currenthealth -= Mathf.Clamp(currenthealth - _damage, 0, startinghealth);
        currenthealth -= _damage;
        if (currenthealth > 0) 
        {
            //player hurt
        }
        else
        {
            //player dead
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
