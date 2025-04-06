using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public class ClickInput : MonoBehaviour
    {
        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    Clickable clickable = hit.collider.GetComponent<Clickable>();
                    if(clickable != null)
                    {
                        clickable.onClick(hit);
                    }
                }
            }
        }
    }

	public interface Clickable
    {
		void onClick(RaycastHit2D hit);
    }
}
