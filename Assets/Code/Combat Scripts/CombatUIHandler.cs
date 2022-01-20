using UnityEngine;
using UnityEngine.UI;
using System;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject PlayerInfoPanel;
    [SerializeField] private GameObject EnemyInfoPanel;
    [SerializeField] private Text Turn;
    [SerializeField] private GameObject MainDecision;
    [SerializeField] private GameObject TargetsPanel;
    [SerializeField] private GameObject ActionsPanel;
    [SerializeField] private GameObject TeamPanel;
    [SerializeField] private Button[] Actions;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private Button TargetButton;

    public void Init()
    {
        /*
        PlayerInfo.text = CombatStateMachine.Instance.PlayerTeam[0].Name + ": " + CombatStateMachine.Instance.PlayerTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[1].Name + ": " + CombatStateMachine.Instance.PlayerTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[2].Name + ": " + CombatStateMachine.Instance.PlayerTeam[2].CurrentHealth;
        EnemyInfo.text = CombatStateMachine.Instance.EnemyTeam[0].Name + ": " + CombatStateMachine.Instance.EnemyTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[1].Name + ": " + CombatStateMachine.Instance.EnemyTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[2].Name + ": " + CombatStateMachine.Instance.EnemyTeam[2].CurrentHealth;
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";
        */
        FillInfoPanels();

        MainDecision.SetActive(true);
        TargetsPanel.SetActive(false);
        ActionsPanel.SetActive(false);
        TeamPanel.SetActive(false);
        BackButton.SetActive(false);

        foreach(Button btn in Actions) 
        {
            btn.onClick.AddListener( () => { OnSelectAction(btn.name); });
        }
    }

    public void UpdateStats() 
    {
        /*
        PlayerInfo.text = CombatStateMachine.Instance.PlayerTeam[0].Name + ": " + CombatStateMachine.Instance.PlayerTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[1].Name + ": " + CombatStateMachine.Instance.PlayerTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.PlayerTeam[2].Name + ": " + CombatStateMachine.Instance.PlayerTeam[2].CurrentHealth;
        EnemyInfo.text = CombatStateMachine.Instance.EnemyTeam[0].Name + ": " + CombatStateMachine.Instance.EnemyTeam[0].CurrentHealth
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[1].Name + ": " + CombatStateMachine.Instance.EnemyTeam[1].CurrentHealth 
        + "\r\n" + CombatStateMachine.Instance.EnemyTeam[2].Name + ": " + CombatStateMachine.Instance.EnemyTeam[2].CurrentHealth;
        Turn.text = CombatStateMachine.Instance.CurrentCharacter.Name + "'s Turn";
        */
    }

    private void FillInfoPanels() 
    {

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
                MainDecision.SetActive(true);
                BackButton.SetActive(false);
                CombatStateMachine.Instance.TargetList.Add(CombatStateMachine.Instance.EnemyTeam[i]);
                CombatStateMachine.Instance.sAttack.Start_StateAttack();
                ClearTargets();
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
    public void OnSelectAction(string btnName) 
    {
        CombatStateMachine.Instance.CurrentCharacterActionIndex = -1;
        for (int i = 1; i < Actions.Length + 1; i++) 
        {
            if (String.Equals(btnName, "btnAction" + i)) 
            {
                CombatStateMachine.Instance.CurrentCharacterActionIndex = i - 1;
            }
        }

        if (CombatStateMachine.Instance.CurrentCharacterActionIndex == -1) 
        {
            throw new Exception("There was an error assigning an action value to the action index");
        }

        ActionsPanel.SetActive(false);
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
        MainDecision.SetActive(false);
        ActionsPanel.SetActive(true);
        FillActionButtonTexts();
        BackButton.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnTeam() 
    {
        MainDecision.SetActive(false);
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
            ActionsPanel.SetActive(true);
        } 
        else if (ActionsPanel.activeSelf) 
        {
            ActionsPanel.SetActive(false);
            MainDecision.SetActive(true);
            BackButton.SetActive(false);
        } 
        else if (TeamPanel.activeSelf) 
        {
            TeamPanel.SetActive(false);
            MainDecision.SetActive(true);
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
