using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject PlayerInfoPanel;
    [SerializeField] private GameObject EnemyInfoPanel;

    [SerializeField] private GameObject[] PlayerCharacterHuds;
    [SerializeField] private GameObject[] EnemyCharacterHuds;
    [SerializeField] private Text[] PlayerCharacterNames;
    [SerializeField] private Text[] EnemyCharacterNames;
    [SerializeField] private Image[] PlayerHealthBars;
    [SerializeField] private Image[] PlayerStaminaBars;
    [SerializeField] private Image[] PlayerManaBars;
    [SerializeField] private Image[] EnemyHealthBars;
    [SerializeField] private Image[] EnemyStaminaBars;
    [SerializeField] private Image[] EnemyManaBars;

    [SerializeField] private GameObject MainDecisionPanel;
    [SerializeField] private GameObject TargetsPanel;
    [SerializeField] private GameObject AttackPanel;
    [SerializeField] private GameObject TeamPanel;
    [SerializeField] private GameObject CombatTextPanel;
    [SerializeField] private Text CombatText;
    [SerializeField] private Button[] Attacks;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private Button TargetButton;

    public void Init()
    {
        ActivateHUDs();
        StartCoroutine(PrintCombatText($"It is { CombatStateMachine.Instance.CurrentCharacter.Name }'s turn!"));

        MainDecisionPanel.SetActive(true);
        TargetsPanel.SetActive(false);
        AttackPanel.SetActive(false);
        TeamPanel.SetActive(false);
        BackButton.SetActive(false);

        foreach(Button btn in Attacks) 
        {
            btn.onClick.AddListener( () => { OnSelectAttack(btn.name); });
        }
    }

    private void ActivateHUDs() 
    {
        for (int k = 0; k < 5; k++) 
        {
            PlayerCharacterHuds[k].SetActive(false);
            EnemyCharacterHuds[k].SetActive(false);
        }

        for (int i = 0; i < CombatStateMachine.Instance.PlayerTeam.Count; i++) 
        {
            PlayerCharacterHuds[i].SetActive(true);
            PlayerCharacterNames[i].text = CombatStateMachine.Instance.PlayerTeam[i].Name;
        }

        for (int j = 0; j < CombatStateMachine.Instance.EnemyTeam.Count; j++) 
        {
            EnemyCharacterHuds[j].SetActive(true);
            EnemyCharacterNames[j].text = CombatStateMachine.Instance.EnemyTeam[j].Name;
        }
    }

    /// <summary>
    /// Function simply updates the health, stamina, and mana bar images in the UI by taking
    /// the character and setting the fill amount of the images to the ratio of their stats.
    /// </summary>
    public void UpdateHUDBars() 
    {
        for (int i = 0; i < CombatStateMachine.Instance.PlayerTeam.Count; i++) 
        {
            float healthRatio = (float)CombatStateMachine.Instance.PlayerTeam[i].CurrentHealth / (float)CombatStateMachine.Instance.PlayerTeam[i].MaxHealth;
            float staminaRatio = (float)CombatStateMachine.Instance.PlayerTeam[i].CurrentStamina / (float)CombatStateMachine.Instance.PlayerTeam[i].MaxStamina;
            float manaRatio = (float)CombatStateMachine.Instance.PlayerTeam[i].CurrentMana / (float)CombatStateMachine.Instance.PlayerTeam[i].MaxMana;
            if (healthRatio < 0) 
                healthRatio = 0;
            if (staminaRatio < 0)
                staminaRatio = 0;
            if (manaRatio < 0)
                manaRatio = 0;
            PlayerHealthBars[i].fillAmount = healthRatio;
            PlayerStaminaBars[i].fillAmount = staminaRatio;
            PlayerManaBars[i].fillAmount = manaRatio;
        }

        for (int j = 0; j < CombatStateMachine.Instance.EnemyTeam.Count; j++) 
        {
            float healthRatio = (float)CombatStateMachine.Instance.EnemyTeam[j].CurrentHealth / (float)CombatStateMachine.Instance.EnemyTeam[j].MaxHealth;
            float staminaRatio = (float)CombatStateMachine.Instance.EnemyTeam[j].CurrentStamina / (float)CombatStateMachine.Instance.EnemyTeam[j].MaxStamina;
            float manaRatio = (float)CombatStateMachine.Instance.EnemyTeam[j].CurrentMana / (float)CombatStateMachine.Instance.EnemyTeam[j].MaxMana;
            if (healthRatio < 0) 
                healthRatio = 0;
            if (staminaRatio < 0)
                staminaRatio = 0;
            if (manaRatio < 0)
                manaRatio = 0;
            EnemyHealthBars[j].fillAmount = healthRatio;
            EnemyStaminaBars[j].fillAmount = staminaRatio;
            EnemyManaBars[j].fillAmount = manaRatio;
        }
    }

    public void EndTurn() 
    {
        MainDecisionPanel.SetActive(true);
    }

    public IEnumerator PrintCombatText(string battleText) 
    {
        foreach (char letter in battleText.ToCharArray()) 
        {
            CombatText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }

    /// <summary>
    /// After choosing the <c>Attack</c> option in the battle menu, the list of actions
    /// the player can make will be shown. While the UI element with the list of actions
    /// is being enabled, this function is called to fill all of the different button's
    /// text with the actions of the current character. All button's text are set to blank
    /// by default and disabled. This is in case a character does not have a full set of
    /// actions yet. The buttons are then enabled and given text based on how many actions
    /// are in the character's <c>ActionList</c>.
    /// </summary>
    private void FillActionButtonTexts() 
    {
        foreach(Button btn in Attacks) 
        {
            btn.GetComponentInChildren<Text>().text = "";
            btn.interactable = false;
        }
        
        for (int i = 0; i < CombatStateMachine.Instance.CurrentCharacter.ActionList.Count; i++) 
        {
            Attacks[i].GetComponentInChildren<Text>().text = CombatStateMachine.Instance.CurrentCharacter.ActionList[i].Name + "\r\n" +
            CombatStateMachine.Instance.CurrentCharacter.ActionList[i].EnergyCost;
            Attacks[i].interactable = true;
        }
    }

    /// <summary>
    /// This function dynamically fills the <c>TargetsPanel</c> object with buttons equal
    /// to the number of enemies in the enemy team. It adjusts for the size in order to
    /// fill the screen and adds the required text and listeners to the buttons.
    /// </summary>
    private void FillTargetButtons() 
    {
        float spacing;
        for (int i = 1; i < CombatStateMachine.Instance.EnemyTeam.Count + 1; i++) 
        {
            spacing = -960f + (1920 / CombatStateMachine.Instance.EnemyTeam.Count * (i - 1)) + ((1920 / CombatStateMachine.Instance.EnemyTeam.Count) / 2);
            Button newButton = Instantiate(TargetButton, new Vector3(spacing, 0f, 0f), Quaternion.identity) as Button;
            newButton.transform.SetParent(TargetsPanel.transform, false);
            newButton.GetComponentInChildren<Text>().text = CombatStateMachine.Instance.EnemyTeam[i - 1].Name;
            newButton.name = "btnTarget" + i;
            newButton.transform.localScale = Vector3.one;
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1920 / CombatStateMachine.Instance.EnemyTeam.Count, 270);
            newButton.onClick.AddListener( () => { OnSelectTarget(newButton.name); });
        }
    }

    /// <summary>
    /// simple functions destroys all of the button objects that are the child
    /// of the <c>TargetsPanel</c> gameObject. This needs to be done because the
    /// buttons are dynamically created every time the panel is activated.
    /// </summary>
    private void ClearTargets() 
    {
        foreach (Transform child in TargetsPanel.transform) 
        {
            Destroy(child.gameObject);
        }
    }

    #region Click Events

    /// <summary>
    /// Once a target is selected by the player, this function is called and the target
    /// selected is determined by the name of the button that was pressed. The index
    /// of the target is then used to access the character in the enemy team and adds
    /// them to the target list. The Attack state is then started.
    /// </summary>
    /// <param name="name"> The name of the button that was pressed. This tells the function 
    /// which target was chosen.</param>
    private void OnSelectTarget(string name) 
    {
        for (int i = 0; i < CombatStateMachine.Instance.EnemyTeam.Count; i++) 
        {
            if (String.Equals(name, "btnTarget" + (i + 1))) 
            {
                TargetsPanel.SetActive(false);
                BackButton.SetActive(false);
                ClearTargets();
                CombatTextPanel.SetActive(true);

                CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[i]);
                CombatStateMachine.Instance.sAttack.Start_StateAttack();
            }
        }
    }

    /// <summary>
    /// After selecting an action, this method is called to check which button was pressed.
    /// Since there are 8 buttons to choose from you can extract which action the player
    /// chose. The 8 actions from the character's action list will be listed in order, so
    /// if button 3 was chosen, we know the player wants to use the character's action at
    /// index[2] (because arrays start at 0, but the numbered buttons start at 1). After 
    /// matching the button name, the action index value is set to i - 1 in the State Machine.
    /// </summary>
    /// <param name="btnName"> The name of the button that was pressed. This tells the function 
    /// which action was chosen</param>
    public void OnSelectAttack(string btnName) 
    {
        for (int i = 1; i < Attacks.Length + 1; i++) 
        {
            if (String.Equals(btnName, "btnAttack" + i)) 
            {
                CombatStateMachine.Instance.CurrentCharacterActionIndex = i - 1;
            }
        }

        AttackPanel.SetActive(false);
        TargetsPanel.SetActive(true);
        FillTargetButtons();
    }

    // Instead of just showing targets, this function should show a list of all the actions the
    // Character can use, THEN after choosing an action the player chooses a target to use the
    // Action on. Since the list of actions will be 0 - whatever, the elements can have the 
    // same value, so if they choose their 0th action, pass a 0 on to the action class so it
    // knows to use the CurrentCharacter's 0th action from their ActionList. boom
    public void OnAttack() 
    {
        CombatTextPanel.SetActive(false);
        MainDecisionPanel.SetActive(false);
        AttackPanel.SetActive(true);
        FillActionButtonTexts();
        BackButton.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnTeam() 
    {
        MainDecisionPanel.SetActive(false);
        CombatTextPanel.SetActive(false);
        TeamPanel.SetActive(true);
        BackButton.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBack() 
    {
        if (TargetsPanel.activeSelf)
        {
            ClearTargets();
            TargetsPanel.SetActive(false);
            AttackPanel.SetActive(true);
        } 
        else if (AttackPanel.activeSelf) 
        {
            AttackPanel.SetActive(false);
            MainDecisionPanel.SetActive(true);
            CombatTextPanel.SetActive(true);
            BackButton.SetActive(false);
        } 
        else if (TeamPanel.activeSelf) 
        {
            TeamPanel.SetActive(false);
            MainDecisionPanel.SetActive(true);
            CombatTextPanel.SetActive(true);
            BackButton.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnFlee() 
    {
        CombatStateMachine.Instance.AttemptToFlee();
        throw new NotImplementedException("Fleeing not implemented");
    }

    #endregion
}
