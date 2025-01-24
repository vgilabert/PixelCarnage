namespace StatSystem
{
    public class Item
    {
        private StatModifier[] _statModifiers;

        public void Equip(Player.Player player)
        {
            foreach (var statModifier in _statModifiers)
            {
                player.Stats[statModifier.StatType].AddModifier(statModifier);
            }
        }
    }
}