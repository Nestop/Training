using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DamageScreen : MonoBehaviour
    {
        private const float LiftingHeightInUnit = 2f;

        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI damageLabel;
        [SerializeField] private RectTransform ui;
        
        private Color _backgroundStartColor;
        private Color _damageLabelStartColor;
        
        private void Start()
        {
            ui.gameObject.SetActive(false);
            _backgroundStartColor = background.color;
            _damageLabelStartColor = damageLabel.color;
        }

        public void ShowDamage(int damage, float timeForShow, Vector3 position, Vector3 lookAt, Action onExit = null)
        {
            transform.position = position;
            transform.LookAt(lookAt);
            damageLabel.text = $"-{damage.ToString()}";
            ui.gameObject.SetActive(true);
            
            StartCoroutine(ExitAnimation(timeForShow, onExit));
        }
        
        private IEnumerator ExitAnimation(float timeForShow, Action onExit = null)
        {
            background.color = _backgroundStartColor;
            damageLabel.color = _damageLabelStartColor;
            
            var deltaPosY = LiftingHeightInUnit / timeForShow;
            var deltaBackAlpha = background.color.a / timeForShow;
            var deltaTextAlpha = damageLabel.color.a / timeForShow;
                
            while (timeForShow > 0)
            {
                timeForShow -= Time.deltaTime;
                transform.position += deltaPosY*Time.deltaTime*Vector3.up;
                var color = background.color;
                color.a -= deltaBackAlpha*Time.deltaTime;
                background.color = color;
                color = damageLabel.color;
                color.a -= deltaTextAlpha*Time.deltaTime;
                damageLabel.color = color;
                yield return null;
            }
            
            ui.gameObject.SetActive(false);
            
            onExit?.Invoke();
        }
    }
}