using UnityEngine;
using RPG.Move;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        [SerializeField] float timeBetweenAttacks = 1f;
        
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Movement>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Movement>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            // Reset the Stop Attack trigger event
            GetComponent<Animator>().ResetTrigger("stopAttack");
            // This will trigger the Attack event
            GetComponent<Animator>().SetTrigger("attack");
        }

        /*void Hit()
        {
            Health healthComp = GetComponent<Health>();
            healthComp.TakeDamage(weaponDamage);
        }*/

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
            /*float enemyPosition = Vector3.Distance(transform.position, target.position);
            return enemyPosition <= weaponRange;*/
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ScheduleAction>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
