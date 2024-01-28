using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUIEffect : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text;
    [SerializeField]
    float animatioTime = 0.5f;
    [SerializeField]
    float movementSpeed = 1;

    bool isAnimating = false;
    float currentTime = 0;

    public void StartAnimation(int a_scoreDiffference)
    {
        text.text = "+ " + a_scoreDiffference + "$";
        isAnimating = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isAnimating)
        {
            currentTime += Time.deltaTime;
            transform.position += transform.up * -1 * movementSpeed * Time.deltaTime;

            if(currentTime >= animatioTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
