using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UISystem
{
    public class SkillsTab : MonoBehaviour
    {
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float closePosition;
        [SerializeField] private GameObject buttonParent;
        [SerializeField] private SkillButton skillButtonPrefab;
        [SerializeField] private Image openIcon;
        [SerializeField] private Image closeIcon;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform rectTransform;
        private Coroutine _tabAnimationCoroutine;
        private bool _open;
        
        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                _open = !_open;
                OpenCloseTabWithAnimation(_open);
                SetButtonIcon(_open);
            });
        }

        public void AddButtonToParent(GameObject button)
        {
            button.transform.SetParent(buttonParent.transform);
        }
        
        public void SetButtonIcon(bool open)
        {
            openIcon.gameObject.SetActive(!open);
            closeIcon.gameObject.SetActive(open);
        }
        
        public void OpenCloseTabWithAnimation(bool open)
        {
            // Play open animation
            if(_tabAnimationCoroutine != null)
                StopCoroutine(_tabAnimationCoroutine);
            _tabAnimationCoroutine = StartCoroutine(OpenCloseAnimation2());
        }

        // public IEnumerator OpenCloseAnimation()
        // {
        //     float t = 0;
        //     float duration = animationDuration;
        //     Vector3 startPosition = rectTransform.localPosition;
        //     Vector3 targetPosition = _open ? new Vector3(0, rectTransform.localPosition.y, rectTransform.localPosition.z) : new Vector3(closePosition, rectTransform.localPosition.y, rectTransform.localPosition.z);
        //     while (t < 1)
        //     {
        //         t += Time.deltaTime / duration;
        //         rectTransform.localPosition = Vector3.Lerp(startPosition,  targetPosition, t);
        //         yield return null;
        //     }
        // }
        public float startX, targetX;
        private IEnumerator OpenCloseAnimation2()
        {
            float t = 0;
            float duration = animationDuration;
            startX = rectTransform.localPosition.x;
            targetX = _open ? 0 : closePosition;
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                rectTransform.localPosition = new Vector3(Mathf.Lerp(startX, targetX, t), rectTransform.localPosition.y, rectTransform.localPosition.z);
                yield return null;
            }
        }
    }
}