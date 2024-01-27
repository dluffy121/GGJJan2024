using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    //[SerializeField]
    //private Text m_livesText;
    [SerializeField]
    Image sirenToShow;
    [SerializeField]
    float m_waitbeforeSwapingSprite = 0.1f;
    [SerializeField]
    Sprite redSirenSprite, blueSirenSprite;
    [SerializeField]
    AudioClip sirenEffectSound;

    int m_livesLost;
    List<Image> m_spawnedSirenImages = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        //m_livesText = gameObject.GetComponent<Text>();
        //m_livesText.text = string.Format("Lives : {0}",5);
    }

    private void Awake()
    {
        GameEvents.OnTotalLivesUpdated += CallbackOnLevelLivesupdated;
    }

    private void OnEnable()
    {
        GameEvents.OnLiveLost += CallbackOnLiveLost;
    }

    private void OnDisable()
    {
        GameEvents.OnLiveLost -= CallbackOnLiveLost;
    }

    private void OnDestroy()
    {
        GameEvents.OnTotalLivesUpdated -= CallbackOnLevelLivesupdated;
    }

    private void CallbackOnLiveLost(int a_Lives)
    {
        //m_livesText.text = string.Format("Lives : {0}", a_Lives);
        StartCoroutine("SirenAnimation");
    }

    public void CallbackOnLevelLivesupdated(int a_lives)
    {
        StopAllCoroutines();
        for(int i = 0; i < m_spawnedSirenImages.Count; i++)
        {
            Destroy(m_spawnedSirenImages[i].gameObject);
        }
        m_spawnedSirenImages.Clear();
        m_livesLost = 0;
        for(int i = 0; i < a_lives; i++)
        {
            Image l_spawnedSiren = Instantiate<Image>(sirenToShow, transform);
            m_spawnedSirenImages.Add(l_spawnedSiren);
        }
    }


    public IEnumerator SirenAnimation()
    {
        SoundManager.PlaySoundEffect(sirenEffectSound);
        for(int i = 0; i < 10; i++)
        {
            m_spawnedSirenImages[m_livesLost].sprite = redSirenSprite;
            yield return new WaitForSeconds(m_waitbeforeSwapingSprite);
            m_spawnedSirenImages[m_livesLost].sprite = blueSirenSprite;
            yield return new WaitForSeconds(m_waitbeforeSwapingSprite);
        }

        m_spawnedSirenImages[m_livesLost].sprite = redSirenSprite;
        m_livesLost++;
    }
}
