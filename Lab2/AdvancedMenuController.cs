using System.Collections.Generic;
using Lab1;

namespace Lab2
{
    class AdvancedMenuController : MenuController
    {
        public AdvancedMenuController(AdvancedPathController pathController) : base(pathController, new List<MenuElement>())
        {
            menuElements.Add(new MenuElement("Create new blocks info", CreateBlocks));
            menuElements.Add(new MenuElement("Load blocks info", LoadBlocks));
        }
        public AdvancedMenuController(AdvancedPathController pathController, List<MenuElement> elements) : base(pathController, elements)
        {
        }

        private void LoadBlocks()
        {
            ((AdvancedPathController)pathController).Load();
            FillMenuElements();
        }

        private void CreateBlocks()
        {
            pathController.ReadBlocksInfo();
            FillMenuElements();
        }

        protected new void FillMenuElements()
        {
            base.FillMenuElements();
            menuElements.Insert(menuElements.Count - 1, new MenuElement("Save", ((AdvancedPathController)pathController).Save));
            menuElements.Insert(menuElements.Count - 1, new MenuElement("Load", ((AdvancedPathController)pathController).Load));
        }
    }
}