using Prototype.UI;
using TMPro;
using UnityEngine.UI;

namespace Prototype
{
    public class PRAgencyUI : UIPage
    {
        public LevelUpUIItem customersMoveSpeedLevelUpUI;
        public LevelUpUIItem customerSpawnSpeedLevelUpUI;
        public TextMeshProUGUI moveSpeedText;
        public TextMeshProUGUI spawnSpeedText;
        private PRAgency m_PRAgency;

        public static PRAgencyUI Instance { get; private set; }
        public Button closeButton;
        protected override void Awake()
        {
            base.Awake();

            customersMoveSpeedLevelUpUI.buyButton.onClick.AddListener(() =>
            {
                m_PRAgency.m_Spanwer.customerMoveSpeed.LevelUp();
            });

            customerSpawnSpeedLevelUpUI.buyButton.onClick.AddListener(() =>
            {
                m_PRAgency.m_Spanwer.customerSpawnSpeed.LevelUp();
            });

            Instance = this;
            closeButton.onClick.AddListener(() =>
            {
                RaycastInput.GetInstance().BlockRaycast = false;
            });
        }

        public void Bind(PRAgency prAgency)
        {
            m_PRAgency = prAgency;
            m_PRAgency.m_Spanwer.customerMoveSpeed.onChanged += UpdateUI;
            m_PRAgency.m_Spanwer.customerSpawnSpeed.onChanged += UpdateUI;

            UpdateUI();
        }

        public override void Show()
        {
            RaycastInput.GetInstance().BlockRaycast = true;
            base.Show();
        }

        public override void Hide(bool onlyDisableRaycast = false)
        {
            base.Hide(onlyDisableRaycast);
        }

        void UpdateUI()
        {
            var customerMoveSpeed = m_PRAgency.m_Spanwer.customerMoveSpeed;
            var customerSpawnSpeed = m_PRAgency.m_Spanwer.customerSpawnSpeed;

            moveSpeedText.text = $"customers move speed: {customerMoveSpeed.GetValue().ToString("0.0")}";
            spawnSpeedText.text = $"customers spawn speed: {m_PRAgency.m_Spanwer.SpawnsPerMinute().ToString("0.0")} p/m";

            customersMoveSpeedLevelUpUI.UpgradeItem(customerMoveSpeed);
            customerSpawnSpeedLevelUpUI.UpgradeItem(customerSpawnSpeed);        
        }
    }
}