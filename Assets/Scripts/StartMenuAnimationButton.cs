using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems; // для работы с событиями UI
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки анимации")]
    public float hoverScale = 1.1f;    // На сколько увеличивать кнопку
    public float animationDuration = 0.2f; // Длительность анимации
    
    private Vector3 _originalScale;    // Исходный масштаб кнопки
    private Button _button;            // Ссылка на компонент Button

    void Start()
    {
        // Сохраняем исходный масштаб
        _originalScale = transform.localScale;
        
        // Получаем компонент кнопки (опционально)
        _button = GetComponent<Button>();
    }

    // Срабатывает при наведении курсора на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Проверяем, активна ли кнопка (опционально)
        if (_button != null && !_button.interactable) return;
        
        // Увеличиваем кнопку с плавной анимацией
        transform.DOScale(_originalScale * hoverScale, animationDuration)
                 .SetEase(Ease.OutBack); // Добавляем эффект "пружинки"
    }

    // Срабатывает при уходе курсора с кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем кнопку к исходному размеру
        transform.DOScale(_originalScale, animationDuration)
                 .SetEase(Ease.OutBack);
    }

    // Опционально: сброс масштаба при деактивации объекта
    void OnDisable()
    {
        // Немедленно возвращаем исходный масштаб
        transform.localScale = _originalScale;
    }
}