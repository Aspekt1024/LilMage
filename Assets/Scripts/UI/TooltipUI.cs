using Aspekt.UI;
using TMPro;
using UnityEngine;

namespace LilMage
{
    public class TooltipUI : UIPanel
    {
#pragma warning disable 649
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
#pragma warning restore 649

        private RectTransform rectTf;

        private void Awake()
        {
            rectTf = GetComponent<RectTransform>();
        }

        public struct Details
        {
            public readonly string Name;
            public readonly string Description;

            public Details(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }
        
        public void Populate(Details details)
        {
            itemName.text = details.Name;
            itemDescription.text = details.Description;
        }

        public void PositionNear(Transform obj)
        {
            float spaceToRight = Screen.width - obj.position.x; // TODO account for camera offset?
            float xPos = obj.position.x;
            if (spaceToRight > rectTf.rect.size.x)
            {
                xPos += rectTf.rect.size.x / 2f;
            }
            else
            {
                xPos -= rectTf.rect.size.x / 2f;
            }

            float yPos = obj.position.y - rectTf.rect.size.y / 2f;
            rectTf.position = new Vector3(xPos, yPos, 0f);
        }
    }
}