using System;
using UnityEngine;
using RPG.Move;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) 
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {         
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Movement>().MoveTo(hit.point);
                }
                Debug.DrawRay(GetMouseRay().origin, GetMouseRay().direction * 100);
                return true;
            }
            return false;            
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }   
}
