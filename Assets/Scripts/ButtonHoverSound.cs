using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour
{
    [SerializeField] private AudioClip hoverSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Добавляем Event Trigger если его нет
        EventTrigger trigger = GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();

        // Создаем entry для события наведения
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerEnter(); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerEnter()
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}