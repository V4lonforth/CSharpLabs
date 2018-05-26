using System.Collections.Generic;
using Lab1.Menu;
using Lab1.Input;
using Lab1.Output;

namespace Lab2.Menu
{
    public class AdvancedMenuController : MenuController
    {
        private IAdvancedMenuActions advancedMenuActions;

        public AdvancedMenuController(IAdvancedMenuActions menuActions, IReader reader, IWriter writer) : base(reader, writer)
        {
            menuElements = new List<MenuElement>()
            {
                new MenuElement("Create new blocks info", CreateBlocks),
                new MenuElement("Load blocks info", LoadBlocks)
            };
            advancedMenuActions = menuActions;
        }
        public AdvancedMenuController(List<MenuElement> elements, IReader reader, IWriter writer) : base(elements, reader, writer)
        {
        }

        private void LoadBlocks()
        {
            advancedMenuActions.Load();
            FillMenuElements(advancedMenuActions);
        }

        private void CreateBlocks()
        {
            advancedMenuActions.CreateBlocks();
            FillMenuElements(advancedMenuActions);
        }

        protected void FillMenuElements(IAdvancedMenuActions advancedMenuActions)
        {
            FillMenuElements((IMenuActions)advancedMenuActions);
            menuElements.Insert(menuElements.Count - 1, new MenuElement("Save", advancedMenuActions.Save));
            menuElements.Insert(menuElements.Count - 1, new MenuElement("Load", advancedMenuActions.Load));
        }
    }
}