using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MechanicsUI : MonoBehaviour
{
    public Image AttackImage;
    public Image PhaseImage;
    public TextMeshProUGUI ScoreUI;
    public int currentScore;

    private void Start()
    {
        ScoreUI.text = currentScore.ToString();
    }

    public void UpdateScore() {
        currentScore++;
        ScoreUI.text= currentScore.ToString();
    }

    public void SetAttackCooldownProgress(float progress)
    {
        AttackImage.fillAmount = progress;
    }

    public void SetPhaseCooldownProgress(float progress)
    {
        PhaseImage.fillAmount = progress;
    }
}
