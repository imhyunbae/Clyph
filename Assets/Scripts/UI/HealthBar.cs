using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text Name;
    Unit Owner;
    public Slider HealthSlider;
    public void Setup(Unit NewOnwer)
    {
        Owner = NewOnwer;
        Name.text = Owner.gameObject.name;
    }

    void Update()
    {
        if (Owner == null)
        {
            Destroy(gameObject);
            return;
        }

        HealthSlider.value = Owner.HP / Owner.MaxHP;
        Vector3 Offset = new Vector3(0, Owner.GetComponent<SpriteRenderer>().bounds.extents.y + 0.05f, 0.0f);
        transform.position = Manager.Instance.Camera.WorldToScreenPoint(Owner.transform.position + Offset);
    }
}
