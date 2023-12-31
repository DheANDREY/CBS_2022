using UnityEngine;
using UnityEngine.UI;
using LevelUpStudio.ChaosBlitzSprint.Player;
using UnityEngine.EventSystems;

namespace LevelUpStudio.ChaosBlitzSprint.UI
{
    public class MainMenuManager : MonoBehaviour
    {

        [SerializeField]private GameObject menuPanel,roundSelectPanel, lobbyPanel, playerReadyMenu;
        [SerializeField]private Button menuFirst, roundSelectFirst, lobbyFirst ;

        public static MainMenuManager Instance { get; private set; }
        private void Awake()
        {
            if(Instance != null)
            {
                Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void EnterRoundSelection()
        {
            menuPanel.SetActive(false);
            roundSelectPanel.SetActive(true);
            roundSelectFirst.Select();    
        }
        public void ExitRoundSelection()
        {
            roundSelectPanel.SetActive(false);
            menuPanel.SetActive(true);
            menuFirst.Select();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonMain);
        }


        public void EnterLobby()
        {
            PlayerConfigurationManager.Instance.EnableJoining();
            roundSelectPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(lobbyFirstButton);
        }

        public void ExitLobbyMenu()
        {
            lobbyPanel.SetActive(false);
            roundSelectPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            roundSelectFirst.Select();
        }

        public GameObject PanelOption, firstButtonOpt, firstButtonMain, lobbyFirstButton;
        public void EnterOptionMenu()
        {
            PanelOption.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonOpt);
        }
        public void ExitOptionMenu()
        {
            PanelOption.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonMain);
        }

        public GameObject PanelCredit, firstButtonCredit;
        public void EnterCreditMenu()
        {
            PanelCredit.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonCredit);
        }
        public void ExitCreditMenu()
        {
            PanelCredit.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonMain);
        }

        public void ExitLobby()
        {
            PlayerConfigurationManager.Instance.DisableJoining();
            PlayerConfigurationManager.Instance.ClearPlayers();
            ClearReadyPlayerMenu();
            lobbyPanel.SetActive(false);
            roundSelectPanel.SetActive(true);
            roundSelectFirst.Select();
        }
        public GameObject exitPanel, firstButtonExit;
        public void openExitPane()
        {
            exitPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonExit);
        }
        public void closeExitPanel()
        {
            exitPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonMain);
        }
        public void ExitGame()
        {
            Application.Quit();
        }

        public void ClearReadyPlayerMenu()
        {
            //Deletes the children of this transform, which are the PlayerConfiguration prefabs
            int i = 0;
            //Array to hold all child obj
            GameObject[] allChildren = new GameObject[playerReadyMenu.transform.childCount];

            //Find all child obj and store to that array
            foreach (Transform child in playerReadyMenu.transform)
            {
                allChildren[i] = child.gameObject;
                i += 1;
            }

            //Now destroy them
            foreach (GameObject child in allChildren)
            {
                Destroy(child.gameObject);
            }
        }

        public void SetRoundType(RoundType rType)
        {
            PlayerConfigurationManager.Instance.roundType = rType;
            EnterLobby();
        }

        [SerializeField]private GameObject startGameButton;
        public GameObject GetStartGameButton(){return startGameButton;}

        public void StartGame()
        {
            PlayerConfigurationManager.Instance?.BeginGame();
            //GameManager.Instance?.PhaseSwitch(GameManager.GameStatus.PickPhase);
        }
    }
}