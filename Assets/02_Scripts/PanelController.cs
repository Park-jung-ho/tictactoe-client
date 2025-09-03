using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    [SerializeField] private RectTransform panelRectTransform;
    
    private CanvasGroup _backgroundCanvasGroup;
    
    /// <summary>
    /// Hide를 실행할 때 해야 할 동작을 추가
    /// </summary>
    public delegate void PanelControllerHideDelegate();
    void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;
        
        _backgroundCanvasGroup.DOFade(1,0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1,0.3f).SetEase(Ease.OutBack);
    }
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;
        
        _backgroundCanvasGroup.DOFade(0,0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0,0.3f).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                hideDelegate?.Invoke();
                Destroy(gameObject);
            });
    }
    
    
}
