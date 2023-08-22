using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PlayerScripts
{
    public class PlayerHealth : MonoBehaviour
    {
        private InputManager _inputManager;
        
        [Header("Health Bar")]
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float chipSpeed = 2f;
        [SerializeField] private float restoreSpeed = 2f;
        [SerializeField] private Image frontHealthBar;
        [SerializeField] private Image backHealthBar;
        private float _health;
        private float _lerpTimer;
        private bool _canUpdateHealthUI;

        [Header("Overlay Effect")] 
        [SerializeField] private Image overlay;
        [SerializeField] private float overlayDuration;
        [SerializeField] private float overlyFadeSpeed;
        [SerializeField] private float overlayAlpha;
        private float _overlayDurationTimer;



        // Start is called before the first frame update
        void Start()
        {
            _health = maxHealth;
            _inputManager = GetComponent<InputManager>();
            _canUpdateHealthUI = false;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        }

        // Update is called once per frame
        void Update()
        {
            _health = Mathf.Clamp(_health, 0, maxHealth);
            UpdateHealthUI();
            UpdateOverlay();

        }
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
            _lerpTimer = 0;
            _canUpdateHealthUI = true;

            _overlayDurationTimer = 0;
            overlay.color = Color.red;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, overlayAlpha);
        }

        public void RestoreHealth(float healthAmount)
        {
            _health += healthAmount;
            _lerpTimer = 0;
            _canUpdateHealthUI = true;
            
            _overlayDurationTimer = 0;
            overlay.color = Color.green;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, overlayAlpha);
        }

        private void UpdateOverlay()
        {
            if (overlay.color.a > 0)
            {
                _overlayDurationTimer += Time.deltaTime;
                
                if (_overlayDurationTimer > overlayDuration)
                {
                    float tempAlpha = overlay.color.a;
                    tempAlpha -= Time.deltaTime * overlyFadeSpeed;
                    overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
                }
            }
        }
        
        private void UpdateHealthUI()
        {
            if (!_canUpdateHealthUI) return;

            float fillF = frontHealthBar.fillAmount;
            float fillB = backHealthBar.fillAmount;
            float hFraction = _health / maxHealth;

            if (fillB > hFraction)
            {
                frontHealthBar.fillAmount = hFraction;
                backHealthBar.color = Color.red;
                _lerpTimer += Time.deltaTime;
                float percentComplete = _lerpTimer / chipSpeed;
                percentComplete *= percentComplete;
                backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
            }

            else if (fillF < hFraction)
            {
                backHealthBar.color = Color.green;
                backHealthBar.fillAmount = hFraction;
                _lerpTimer += Time.deltaTime;
                float percentComplete = _lerpTimer / restoreSpeed;
                percentComplete *= percentComplete;
                frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
                return;
            }

            if (fillB == hFraction && fillF == hFraction )
            {
                _canUpdateHealthUI = false;
            }
        }
        
    }
}
