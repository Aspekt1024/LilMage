using System;
using LilMage.Units;
using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// The default player controller that possesses
    /// </summary>
    public class PlayerController : MonoBehaviour, IController
    {
        private Hero hero;
        private int playerID;
        
        private Rewired.Player player;
        private ClickHandler clickHandler;
        
        private enum States
        {
            None, Active, Inactive
        }
        private States state = States.None;

        public void Init(Hero hero, int playerID)
        {
            this.playerID = playerID;
            this.hero = hero;
            GameManager.Units.SetPlayerHero(hero);
            
            player = Rewired.ReInput.players.GetPlayer(playerID);
            clickHandler = new ClickHandler(player);

            state = States.Active;
        }

        private void Update()
        {
            if (state != States.Active) return;

            // TODO game states

            clickHandler.HandleClicks();

            HandleMovement();
            HandleRotation();
            HandleActions();
        }

        private void HandleMovement()
        {
            float forwardMovement = player.GetAxis(InputBindings.MoveForward);
            float horizontalMovement = player.GetAxis(InputBindings.MoveHorizontal);

            if (Math.Abs(forwardMovement) > 0.1f || Math.Abs(horizontalMovement) > 0.1f)
            {
                hero.Movement.Move(new Vector3(horizontalMovement, 0f, forwardMovement));
            }
            else
            {
                hero.Movement.Stop();
            }
        }

        private void HandleRotation()
        {
            float rotation = player.GetAxis(InputBindings.Rotate);
            if (Math.Abs(rotation) < 0.01f) return;
            hero.Rotation.Rotate(rotation);
        }

        private void HandleActions()
        {
            if (player.GetButtonUp(InputBindings.Action1))
            {
                hero.Attack();
            }
            else if (player.GetButtonUp(InputBindings.Action2))
            {
                hero.ReplenishMana();
            }
        }
    }
    
}