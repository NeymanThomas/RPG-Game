using UnityEngine;
using UnityEngine.UI;
using System;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private Text PlayerInfo;
    [SerializeField] private Text EnemyInfo;
    [SerializeField] private Text Turn;
    [SerializeField] private GameObject MainDecision;
    [SerializeField] private GameObject Targets;
    [SerializeField] private GameObject ActionsPanel;
    [SerializeField] private Button[] Actions;
    [SerializeField] private GameObject BackButton;

    public void Init()
    {
        PlayerInfo.text = CombatStateMachine.Instance.PlayerTeam[0].Name + ": " + CombatStateMachine.Instance.PlayerTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[1].Name + ": " + CombatStateMachine.Instance.PlayerTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[2].Name + ": " + CombatStateMachine.Instance.PlayerTeam[2].CurrentHealth;
        EnemyInfo.text = CombatStateMachine.Instance.EnemyTeam[0].Name + ": " + CombatStateMachine.Instance.EnemyTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[1].Name + ": " + CombatStateMachine.Instance.EnemyTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[2].Name + ": " + CombatStateMachine.Instance.EnemyTeam[2].CurrentHealth;
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";

        MainDecision.SetActive(true);
        Targets.SetActive(false);
        ActionsPanel.SetActive(false);
        BackButton.SetActive(false);

        foreach(Button btn in Actions) 
        {
            btn.onClick.AddListener( () => { OnSelectAction(btn.name); });
        }
    }

    public void UpdateStats() 
    {
        PlayerInfo.text = CombatStateMachine.Instance.PlayerTeam[0].Name + ": " + CombatStateMachine.Instance.PlayerTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[1].Name + ": " + CombatStateMachine.Instance.PlayerTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[2].Name + ": " + CombatStateMachine.Instance.PlayerTeam[2].CurrentHealth;
        EnemyInfo.text = CombatStateMachine.Instance.EnemyTeam[0].Name + ": " + CombatStateMachine.Instance.EnemyTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[1].Name + ": " + CombatStateMachine.Instance.EnemyTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[2].Name + ": " + CombatStateMachine.Instance.EnemyTeam[2].CurrentHealth;
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";
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
        foreach(Button btn in Actions) 
        {
            btn.GetComponentInChildren<Text>().text = "";
            btn.interactable = false;
        }
        
        for (int i = 0; i < CombatStateMachine.Instance.CurrentCharacter.ActionList.Count; i++) 
        {
            Actions[i].GetComponentInChildren<Text>().text = CombatStateMachine.Instance.CurrentCharacter.ActionList[i].Name;
            Actions[i].interactable = true;
        }
    }

    #region Click Events

    public void OnSelectTarget1() 
    {
        Targets.SetActive(false);
        MainDecision.SetActive(true);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[0]);
        CombatStateMachine.Instance.sAttack.Start_StateAttack();
    }

    public void OnSelectTarget2() 
    {
        Targets.SetActive(false);
        MainDecision.SetActive(true);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[1]);
        CombatStateMachine.Instance.sAttack.Start_StateAttack();
    }

    public void OnSelectTarget3() 
    {
        Targets.SetActive(false);
        MainDecision.SetActive(true);
        CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[2]);
        CombatStateMachine.Instance.sAttack.Start_StateAttack();
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
    public void OnSelectAction(string btnName) 
    {
        CombatStateMachine.Instance.CurrentCharacterActionIndex = -1;
        Debug.Log(CombatStateMachine.Instance.CurrentCharacterActionIndex + " after declaration");

        for (int i = 1; i < Actions.Length + 1; i++) 
        {
            if (String.Equals(btnName, "btnAction" + i)) 
            {
                CombatStateMachine.Instance.CurrentCharacterActionIndex = i - 1;
            }
        }

        if (CombatStateMachine.Instance.CurrentCharacterActionIndex == -2) 
        {
            //throw new Exception("Error setting the value of the action index in the OnSelectAction method");
            Debug.Log("Error setting the value of the action index in the OnSelectAction method");
        }
        Debug.Log(CombatStateMachine.Instance.CurrentCharacterActionIndex + " after loop");

        ActionsPanel.SetActive(false);
        Targets.SetActive(true);
    }

    // Instead of just showing targets, this function should show a list of all the actions the
    // Character can use, THEN after choosing an action the player chooses a target to use the
    // Action on. Since the list of actions will be 0 - whatever, the elements can have the 
    // same value, so if they choose their 0th action, pass a 0 on to the action class so it
    // knows to use the CurrentCharacter's 0th action from their ActionList. boom
    public void OnAttack() 
    {
        MainDecision.SetActive(false);
        ActionsPanel.SetActive(true);
        FillActionButtonTexts();
        BackButton.SetActive(true);
    }

    public void OnBack() 
    {
        if (Targets.activeSelf)
        {
            Targets.SetActive(false);
            ActionsPanel.SetActive(true);
        } 
        else if (ActionsPanel.activeSelf) 
        {
            ActionsPanel.SetActive(false);
            MainDecision.SetActive(true);
            BackButton.SetActive(false);
        }
    }

    #endregion
}
