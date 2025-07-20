using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject PlayBtn;
    public Image CircleMaskObj;
    public GameObject PauseCanvas;
    public GameObject SFXBtn;
    public GameObject MusicBtn;
    public GameObject ContinuesBtn;
    public GameObject PauseBtn;
    public GameObject BackToHome;
    public GameObject GameOverPanel;
    public TextMeshProUGUI LevelText;
    public List<GameObject> ListObjInLevel;
    public List<GameObject> ListObjInMenu;
    public GameObject CompletePanel;
    public Sequence SequenceCircleEffect;
    public bool isMusicOn = true;
    public bool isSFXOn = true;
    public Sprite SpriteMusicOn;
    public Sprite SpriteSFXOn;
    public Sprite SpriteMusicOff;
    public Sprite SpriteSFXOff;


    private void Awake()
    {

    }
    public void Start()
    {
        PauseCanvas.SetActive(false);
        CompletePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        TurnOffObjInLevel();
        TurnOnObjInMenu();
        ObserverManager.AddListener("Level Complete", TurnOnCompletePanel);
        ObserverManager.AddListener("Game Over", TurnOnGameOverPanel);



    }
    public void TurnOnGameOverPanel()
    {
        CompletePanel.SetActive(true);



    }
    public void TurnOffObjInLevel()
    {
        for (int i = 0; i < ListObjInLevel.Count; i++)
        {
            ListObjInLevel[i].SetActive(false);
        }

    }
    public void TurnOnObjInLevel()
    {
        for (int i = 0; i < ListObjInLevel.Count; i++)
        {
            ListObjInLevel[i].SetActive(true);
        }

    }
    public void TurnOffObjInMenu()
    {
        for (int i = 0; i < ListObjInMenu.Count; i++)
        {
            ListObjInMenu[i].SetActive(false);
        }

    }
    public void TurnOnObjInMenu()
    {
        for (int i = 0; i < ListObjInMenu.Count; i++)
        {
            ListObjInMenu[i].SetActive(true);
        }

    }
    public void TurnOnCompletePanel()
    {

        CompletePanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseCanvas.SetActive(true);
        PauseBtn.GetComponent<Button>().interactable = false;
        PlayBtn.GetComponent<Button>().interactable = false;
        ObserverManager.Notify("Pause");


    }
    public void Continues()
    {
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
        PauseBtn.GetComponent<Button>().interactable = true;
        PlayBtn.GetComponent<Button>().interactable = true;
        ObserverManager.Notify("Continues");

    }
    public void PlayBtnAction()
    {
        CircleMaskZoomToLevel();
        // LevelManager.Instance.Level++;
        LevelText.text = $"Lv.{LevelManager.Instance.Level}";
        CompletePanel.SetActive(false);
        GameOverPanel.SetActive(false);



    }
    public void NextLevel()
    {
        CircleMaskZoomToLevel();
        LevelManager.Instance.Level++;
        LevelText.text = $"Lv.{LevelManager.Instance.Level}";
        CompletePanel.SetActive(false);
        GameOverPanel.SetActive(false);



    }
    public void NextLevelWhenGameOver()
    {
        CircleMaskZoomToLevel();
        LevelManager.Instance.Level = 1;
        LevelText.text = $"Lv.{LevelManager.Instance.Level}";
        CompletePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        RectTransform rt = CircleMaskObj.GetComponent<RectTransform>();
        Vector2 _default = rt.sizeDelta;
        SequenceCircleEffect = DOTween.Sequence();
        SequenceCircleEffect.Append(rt.DOSizeDelta(Vector2.zero, 1)
               .SetEase(Ease.OutQuad)
               .OnComplete(() =>
               {
                   if (Vector2.Distance(rt.sizeDelta, Vector2.zero) < 0.01f)
                   {
                       ObserverManager.Notify("BackToMenu");
                       CameraController.Instance.SetPotionShelfToCamera(GameObjectStorage.Instance.MiddleSceneCamera, 8);
                       PlayBtn.SetActive(true);
                       TurnOnObjInMenu();
                       TurnOffObjInLevel();
                       CompletePanel.SetActive(false);
                       GameOverPanel.SetActive(false);
                       PauseCanvas.SetActive(false);
                       PauseBtn.GetComponent<Button>().interactable = true;
                       PlayBtn.GetComponent<Button>().interactable = true;
                       Destroy(LevelManager.Instance.CurrentPotionShelf);
                       for (int i = 0; i < LevelManager.Instance.ListPotionInLevel.Count; i++)
                       {
                           Destroy(LevelManager.Instance.ListPotionInLevel[i]);
                       }




                   }

               }));
        SequenceCircleEffect.AppendInterval(1f);
        SequenceCircleEffect.Append(rt.DOSizeDelta(_default, 1).SetEase(Ease.OutQuad));


    }
    public void OnBtnMusic()
    {
        if (isMusicOn == true)
        {
            SoundController.Instance.audioSourceMusic.volume = 0;
            isMusicOn = false;
            MusicBtn.GetComponent<Image>().sprite = SpriteMusicOff;


        }
        else
        {
            SoundController.Instance.audioSourceMusic.volume = 1;
            isMusicOn = true;
            MusicBtn.GetComponent<Image>().sprite = SpriteMusicOn;

        }

    }
    public void OnBtnSFX()
    {
        if (isSFXOn == true)
        {
            SoundController.Instance.audioSourceSFX.volume = 0;
            isSFXOn = false;
            SFXBtn.GetComponent<Image>().sprite = SpriteSFXOff;


        }
        else
        {
            SoundController.Instance.audioSourceSFX.volume = 1;
            isSFXOn = true;
            SFXBtn.GetComponent<Image>().sprite = SpriteSFXOn;

        }

    }


    public void CircleMaskZoomToLevel()
    {

        RectTransform rt = CircleMaskObj.GetComponent<RectTransform>();
        Vector2 _default = rt.sizeDelta;
        SequenceCircleEffect = DOTween.Sequence();

        SequenceCircleEffect.Append(rt.DOSizeDelta(Vector2.zero, 1)
               .SetEase(Ease.OutQuad)
               .OnComplete(() =>
               {
                   if (Vector2.Distance(rt.sizeDelta, Vector2.zero) < 0.01f)
                   {
                       ObserverManager.Notify("New Level");
                       PlayBtn.SetActive(false);
                       TurnOffObjInMenu();
                       TurnOnObjInLevel();

                   }

               }));
        SequenceCircleEffect.AppendInterval(1f);
        SequenceCircleEffect.Append(rt.DOSizeDelta(_default, 1).SetEase(Ease.OutQuad));




    }
}
