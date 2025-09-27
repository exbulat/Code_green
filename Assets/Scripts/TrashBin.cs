using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    [Tooltip("Тип мусора: plastic, glass, paper, organic")]
    public string trashType;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        TrashItem item = dropped.GetComponent<TrashItem>();
        if (item == null) return;

        if (item.trashType == trashType)
        {
            // правильный бросок
            GameManager.instance.OnCorrectDrop(); // сам добавит очки
            Destroy(dropped);
        }
        else
        {
            // неправильный — централизованная обработка штрафа
            GameManager.instance.OnWrongDrop();
            // можно также анимировать "удар" по объекту здесь
        }
    }
}
