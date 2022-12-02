namespace WarCroft.Entities.Characters
{
    using Contracts;
    using Inventory;
 
    public class Priest : Character, IHealer
    {
        public Priest(string name) : base(name, 50, 25, 40, new Backpack())
        {
        }

        public void Heal(Character character)
        {
            this.EnsureAlive();
            character.EnsureAlive();

            character.Health += this.AbilityPoints;
        }
    }
}
