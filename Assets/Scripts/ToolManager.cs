using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;
    public enum Tools 
    {
        Create,
        Form,
        Color,
        Erase,
        Other,
        None
    }

    public SpawnManager spawnManager;
    public ColorManager colorManager;

    private StateMachine<Tools> stateMachine;
    
    private void Awake() 
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }

        stateMachine = new StateMachine<Tools>(this); 
        stateMachine.ChangeState(Tools.None); 
    }

    public void ChangeTool(Tools tool) 
    {
        stateMachine.ChangeState(tool);
    }

    private void Create_Enter()
    {
        print("entered create");
        spawnManager.AddCreateListeners();
        UIManager.Instance.ChangeCreateButtonToSelectedMode(true);
    }
    private void Create_Exit()
    {
        print("exited create");
        spawnManager.RemoveCreateListeners();
        UIManager.Instance.ChangeCreateButtonToSelectedMode(false);
    }


    private void Form_Enter()
    {
        print("entered Form");
        UIManager.Instance.ChangeFormButtonToSelectedMode(true);
    }
    private void Form_Exit()
    {
        print("exited Form");
        UIManager.Instance.ChangeFormButtonToSelectedMode(false);
    }


    private void Color_Enter()
    {
        print("entered Color");
        colorManager.AddListeners();
    }
    private void Color_Exit()
    {
        print("exited Color");
        colorManager.RemoveListeners();
        UIManager.Instance.ShowColorPopUp(false);
    }


    private void Erase_Enter()
    {
        print("entered Erase");
        spawnManager.AddEraseListeners();
        UIManager.Instance.ChangeEraseButtonToSelectedMode(true);
    }
    private void Erase_Exit()
    {
        print("exited Erase");
        spawnManager.RemoveEraseListeners();
        UIManager.Instance.ChangeEraseButtonToSelectedMode(false);
    }


    private void Other_Enter()
    {
        print("entered Other");
    }
    private void Other_Exit()
    {
        print("exited Other");
        UIManager.Instance.ShowOtherPopUp(false);
    }


    private void None_Enter()
    {
        print("entered None");
    }
    private void None_Exit()
    {
        print("exited None");
    }
}
