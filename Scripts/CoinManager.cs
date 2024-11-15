using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int CoinCounter = 0;
    public Text Points;
    void Update()
    {
        Points.text = CoinCounter.ToString();
    }
}
