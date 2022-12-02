using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private List<Character> characters;
		private List<Item> items;

		public WarController()
		{
			characters = new List<Character>();
			items = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];

			Character newCharacter = null;
			switch (characterType)
			{
				case "Priest":
					newCharacter = new Priest(name);
					break;
				case "Warrior":
					newCharacter = new Warrior(name);
					break;
                default:
					throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));				
			}

			characters.Add(newCharacter);
			return string.Format(SuccessMessages.JoinParty, name);
		}

		public string AddItemToPool(string[] args)
		{
			string itemType = args[0];

			Item newItem = null;
			switch (itemType)
			{
				case "HealthPotion":
					newItem = new HealthPotion();
				break;
				case "FirePotion":
					newItem = new FirePotion();
					break;
                default:
					throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemType));				
			}

			items.Add(newItem);
			return string.Format(SuccessMessages.AddItemToPool, itemType);
		}

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];

			var characterToPick = characters.FirstOrDefault(ch => ch.Name == characterName);
			if (characterToPick == null)
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));

			if (!items.Any())
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);

			var itemToPick = items.Last();

			characterToPick.Bag.AddItem(itemToPick);
			items.Remove(itemToPick);

			return string.Format(SuccessMessages.PickUpItem, characterName, itemToPick.GetType().Name);
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];

			var characterToUse = characters.FirstOrDefault(ch => ch.Name == characterName);
			if (characterToUse == null)
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));

			var itemToUse = characterToUse.Bag.GetItem(itemName);
			characterToUse.UseItem(itemToUse);

			return string.Format(SuccessMessages.UsedItem, characterName, itemName);
		}

		public string GetStats()
		{
			var sb = new StringBuilder();

			foreach (var character in characters.OrderByDescending(ch => ch.IsAlive).ThenByDescending(ch => ch.Health))
			{
				string isAllive = character.IsAlive ? "Alive" : "Dead";
				sb.AppendLine($"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: {isAllive}");
			}

			return sb.ToString().TrimEnd();
		}

		public string Attack(string[] args)
		{
			string attackerName = args[0];
			string receiverName = args[1];

			var attacker = characters.FirstOrDefault(ch => ch.Name == attackerName);
			var receiver = characters.FirstOrDefault(ch => ch.Name == receiverName);

			if (attacker == null)
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
			if (receiver == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
			if (attacker.GetType().Name != "Warrior")
				throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));

			(attacker as Warrior).Attack(receiver);

            string output = string.Format(SuccessMessages.AttackCharacter,
                attackerName, receiverName, attacker.AbilityPoints, receiverName, receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor);

            if (!receiver.IsAlive)
                output += Environment.NewLine + string.Format(SuccessMessages.AttackKillsCharacter, receiverName);

            return output;
        }

		public string Heal(string[] args)
		{
            string healerName = args[0];
            string healingReceiverName = args[1];

            Character healer = characters.Find(x => x.Name == healerName);
            Character receiver = characters.Find(x => x.Name == healingReceiverName);

            if (healer == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            if (receiver == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiverName));
            if (healer.GetType().Name != "Priest")
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));

            (healer as Priest).Heal(receiver);

            return string.Format(SuccessMessages.HealCharacter,
                healerName, receiver.Name, healer.AbilityPoints, receiver.Name, receiver.Health);
        }
	}
}
