using Lab1.Menu;

namespace Lab2.Menu
{
    public interface IAdvancedMenuActions : IMenuActions
    {
        void CreateBlocks();
        void Save();
        void Load();
    }
}
