using Prototype.UI;
using TMPro;
using UnityEngine.UI;

namespace Prototype
{
    public class TraderUpgradeUI : UIPage
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI infoTitle;
        public Slider levelupProgress;
        public TextMeshProUGUI currentLevel;
        public TextMeshProUGUI maxLevel;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI levelupMult;

        public LevelUpUIItem costLevelUp;
        public LevelUpUIItem workerLevelUp;
        public LevelUpUIItem newWorkerLevelUp;

        private CostUpgradeData m_CostUpgrade;
        private UpgradeData m_WorkerUpgrade;
        private UpgradeData m_AddWorkerUpgrade;

        private TradingSpot m_Tarder;
        private PlayerData m_Playerdata;
        public Button closeButton;
        public static TraderUpgradeUI Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            closeButton.onClick.AddListener(() =>
            {
                RaycastInput.GetInstance().BlockRaycast = false;
            });

            costLevelUp.buyButton.GetComponent<HoldedButton>().onClick += () =>
            {
                PlayerData.GetInstance().DecreaseMoney(m_CostUpgrade.currentBuyCost);
                m_CostUpgrade.LevelUp();
            };

            workerLevelUp.buyButton.GetComponent<HoldedButton>().onClick += () =>
            {
                PlayerData.GetInstance().DecreaseMoney(m_WorkerUpgrade.GetCostValue());
                m_WorkerUpgrade.LevelUp();
            };

            newWorkerLevelUp.buyButton.GetComponent<HoldedButton>().onClick += () =>
            {
                PlayerData.GetInstance().DecreaseMoney(m_AddWorkerUpgrade.GetCostValue());
                m_AddWorkerUpgrade.LevelUp();
            };

            m_Playerdata = PlayerData.GetInstance();
            
            Instance = this;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void Show()
        {
            base.Show();
            if (m_Playerdata)
            {
                m_Playerdata.onMoneyChanged += TraderUpgradeUI_onMoneyChanged;
            }
           
            RaycastInput.GetInstance().BlockRaycast = true;
        }

        public override void Hide(bool onlyDisableRaycast = false)
        {
            base.Hide(onlyDisableRaycast);
            if (m_Playerdata)
            {
                m_Playerdata.onMoneyChanged -= TraderUpgradeUI_onMoneyChanged;
            }
        }

        private void TraderUpgradeUI_onMoneyChanged(float obj)
        {
            UpdateUI();
        }

        public void Bind(TradingSpot tarder)
        {
            if (m_CostUpgrade != null)
            {
                m_CostUpgrade.onUpgraded -= UpdateUI;
                m_WorkerUpgrade.onChanged -= UpdateUI;
                m_AddWorkerUpgrade.onChanged -= UpdateUI;
                m_Tarder = null;
            }

            m_CostUpgrade = tarder.costUpgrade;
            m_WorkerUpgrade = tarder.workerSpeedUpgrade;
            m_AddWorkerUpgrade = tarder.addWorkerUpgrade;

            m_Tarder = tarder;
            m_CostUpgrade.onUpgraded += UpdateUI;
            m_WorkerUpgrade.onChanged += UpdateUI;
            m_AddWorkerUpgrade.onChanged += UpdateUI;

            costLevelUp.title.text = m_CostUpgrade.upgradeUiTitleName;

            UpdateUI();
        }

        void UpdateUI()
        {
            titleText.text = m_Tarder.traderName;
          
            int prevMax = m_CostUpgrade.GetPrevMax();
            int nextMax = m_CostUpgrade.GetNextMax();

            levelupProgress.minValue = prevMax;
            levelupProgress.maxValue = nextMax;
            int currentCostLevel = m_CostUpgrade.currentLevel;
            levelupProgress.value = currentCostLevel;
            costText.text = m_CostUpgrade.GetProducCost().ToString("0.0");
            infoTitle.text = m_CostUpgrade.GetUpgradeName();
            currentLevel.text = nextMax == currentCostLevel ? "MAX" : currentCostLevel.ToString();
            maxLevel.text = nextMax == currentCostLevel ? "MAX" : nextMax.ToString();
            levelupMult.text = m_CostUpgrade.GetNextUpgradeMult();
          
         
            timeText.text = m_WorkerUpgrade.GetValue().ToString("0.0");

            costLevelUp.cost.text = TextUtils.ValueToShortString(m_CostUpgrade.currentBuyCost);
            costLevelUp.buyButton.interactable = PlayerData.GetInstance().GetMoney() >= m_CostUpgrade.currentBuyCost && !m_CostUpgrade.IsFinished();
            costLevelUp.buyButton.gameObject.SetActive(!m_CostUpgrade.IsFinished());


            workerLevelUp.UpgradeItem(m_WorkerUpgrade);
            newWorkerLevelUp.UpgradeItem(m_AddWorkerUpgrade);
        }
    }
}