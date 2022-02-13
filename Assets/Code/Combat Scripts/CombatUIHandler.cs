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
        CombatStateMachine.Instance.AddCombatText($"It is { CombatStateMachine.Instance.CurrentCharacter.Name }'s turn!");
        CombatStateMachine.Instance.StartCombatText();

        // Check to see who is going first, the enemy or the player
        if (CombatStateMachine.Instance.EnemyTeam.Contains(CombatStateMachine.Instance.CurrentCharacter)) 
        {
            MainDecisionPanel.SetActive(false);
            CombatStateMachine.Instance.ModifyTextState(CombatTextState.EnemyDecision);
        }
        else 
        {
            SetupPlayerDecision();
            CombatStateMachine.Instance.ModifyTextState(CombatTextState.PlayerDecision);
        }

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
        UpdateHUDBars();
    }

    /// <summary>
    /// Function simply updates the health, stamina, and mana bar images in the UI by taking
    /// the character and setting the fill amount of the images to the ratio of their stats.
    /// </summary>
    public void UpdateHUDBars() 
    {
        for (int i = 0; i < CombatStateMachine.Instance.PlayerTeam.Count; i++) 
        {
            if (CombatStateMachine.Instance.PlayerTeam[i].IsAlive) 
            {
                float healthRatio = (float)CombatStateMachine.Instance.PlayerTeam[i].CurrentHealth / (float)CombatStateMachine.Instance.PlayerTeam[i].MaxHealth;
                float staminaRatio = (float)CombatStateMachine.Instance.PlayerTeam[i].CurrentStamina / (float)CombatStateMachine.Instance.PlayerTeam[i].MaxStamina;
                float manaRatio = (float)CombatStateMachine.Instance.PlayerTeam[i].CurrentMana / (float)CombatStateMachine.Instance.PlayerTeam[i].MaxMana;
                // The less than 0 check is just extra assurance and safety it won't come across a random error and break
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
            else 
            {
                PlayerHealthBars[i].fillAmount = 0;
                PlayerStaminaBars[i].fillAmount = 0;
                PlayerManaBars[i].fillAmount = 0;
            }
        }

        for (int j = 0; j < CombatStateMachine.Instance.EnemyTeam.Count; j++) 
        {
            if (CombatStateMachine.Instance.EnemyTeam[j].IsAlive) 
            {
                float healthRatio = (float)CombatStateMachine.Instance.EnemyTeam[j].CurrentHealth / (float)CombatStateMachine.Instance.EnemyTeam[j].MaxHealth;
                float staminaRatio = (float)CombatStateMachine.Instance.EnemyTeam[j].CurrentStamina / (float)CombatStateMachine.Instance.EnemyTeam[j].MaxStamina;
                float manaRatio = (float)CombatStateMachine.Instance.EnemyTeam[j].CurrentMana / (float)CombatStateMachine.Instance.EnemyTeam[j].MaxMana;
                // The less than 0 check is just extra assurance and safety it won't come across a random error and break
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
            else 
            {
                EnemyHealthBars[j].fillAmount = 0;
                EnemyStaminaBars[j].fillAmount = 0;
                EnemyManaBars[j].fillAmount = 0;
            }
        }
    }

    /// <summary>
    /// Sets up the Main UI Panel and disables the CombatTextButton for the player.
    /// </summary>
    public void SetupPlayerDecision() 
    {
        MainDecisionPanel.SetActive(true);
        CombatTextPanel.GetComponentInChildren<Button>().interactable = false;
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
    /// to the number of alive enemies in the enemy team. It adjusts for the size in order to
    /// fill the screen and adds the required text and listeners to the buttons.
    /// </summary>
    private void FillTargetButtons() 
    {
        int aliveEnemies = 0;
        foreach (Character c in CombatStateMachine.Instance.EnemyTeam) 
        {
            if (c.IsAlive) 
            {
                aliveEnemies++;
            }
        }

        float spacing;
        int j = 0;
        for (int i = 1; i < CombatStateMachine.Instance.EnemyTeam.Count + 1; i++)
        {
            // If the enemy indexed is not alive, don't make a button for them to be targeted. 
            if (CombatStateMachine.Instance.EnemyTeam[i - 1].IsAlive)
            {
                spacing = -960f + (1920 / aliveEnemies * (j)) + ((1920 / aliveEnemies) / 2);
                Button newButton = Instantiate(TargetButton, new Vector3(spacing, 0f, 0f), Quaternion.identity) as Button;
                newButton.transform.SetParent(TargetsPanel.transform, false);
                newButton.GetComponentInChildren<Text>().text = CombatStateMachine.Instance.EnemyTeam[i - 1].Name;
                newButton.name = "btnTarget" + i;
                newButton.transform.localScale = Vector3.one;
                newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1920 / aliveEnemies, 270);
                newButton.onClick.AddListener( () => { OnSelectTarget(newButton.name); });
                // j variable is specifically here just to make sure the spacing for the buttons 
                // is correct. Can't go off i loop variable because it goes off the Count of the
                // EnemyTeam, which never changes depending on if the enemies are alive or dead.
                j++;
            }
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
    /// Calls the AdvanceCombat function in the state machine in order to go to
    /// the next stage of combat. This function is called when the player clicks on the 
    /// combat text.
    /// </summary>
    public void OnSelectCombatText() 
    {
        CombatStateMachine.Instance.AdvanceCombat();
    }

    /// <summary>
    /// This coroutine clears the combat text field then transforms the text parameter into a
    /// char array. Then one by one, each char is added to the CombatText field until the
    /// string is completely written.
    /// </summary>
    /// <param name="text">The string of text that will be printed</param>
    /// <returns>The time interval that is held before the next character is printed</returns>
    public IEnumerator PrintCombatText(string text) 
    {
        CombatText.text = "";
        foreach (char letter in text.ToCharArray()) 
        {
            CombatText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

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
                CombatTextPanel.GetComponentInChildren<Button>().interactable = true;

                CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[i]);
                CombatStateMachine.Instance.AdvanceCombat();
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
                if (CombatStateMachine.Instance.CheckForEnoughEnergy()) 
                {
                    AttackPanel.SetActive(false);
                    TargetsPanel.SetActive(true);
                    FillTargetButtons();
                }
                else 
                {
                    CombatStateMachine.Instance.AddCombatText(CombatStateMachine.Instance.CurrentCharacter.Name + " doesn't have enough energy to use " +
                    CombatStateMachine.Instance.CurrentCharacter.ActionList[CombatStateMachine.Instance.CurrentCharacterActionIndex].Name);
                    CombatStateMachine.Instance.ModifyTextState(CombatTextState.NotEnoughEnergy);
                    AttackPanel.SetActive(false);
                    BackButton.SetActive(false);
                    CombatTextPanel.SetActive(true);
                    CombatTextPanel.GetComponentInChildren<Button>().interactable = true;
                    CombatStateMachine.Instance.StartCombatText();
                }
            }
        }
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
        CombatTextPanel.GetComponentInChildren<Button>().interactable = false;
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