using Gapura.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    public class Menu1Controller : Controller
    {
        //
        // GET: /Home/
        [ChildActionOnly]
        public ActionResult Index()
        {
            Menu1Model ObjMenuModel = new Menu1Model();
            ObjMenuModel.MainMenuModel = new List<MainMenu>();
            ObjMenuModel.MainMenuModel = GetMainMenu();
            ObjMenuModel.SubMenuModel = new List<SubMenu>();
            ObjMenuModel.SubMenuModel = GetSubMenu();

            return View(ObjMenuModel);
        }

        public List<MainMenu> GetMainMenu()
        {
            List<MainMenu> ObjMainMenu = new List<MainMenu>();
            ObjMainMenu.Add(new MainMenu { ID = 1, MainMenuItem = "Master", MainMenuURL = "#" });
            ObjMainMenu.Add(new MainMenu { ID = 2, MainMenuItem = "TBA1", MainMenuURL = "#" });
            ObjMainMenu.Add(new MainMenu { ID = 3, MainMenuItem = "TBA2", MainMenuURL = "#" });
            ObjMainMenu.Add(new MainMenu { ID = 4, MainMenuItem = "About", MainMenuURL = "#" });

            return ObjMainMenu;
        }
        public List<SubMenu> GetSubMenu()
        {
            List<SubMenu> ObjSubMenu = new List<SubMenu>();
            ObjSubMenu.Add(new SubMenu { MainMenuID = 1, SubMenuItem = "Category", SubMenuURL = "#" });
            ObjSubMenu.Add(new SubMenu { MainMenuID = 1, SubMenuItem = "Item", SubMenuURL = "#" });
            ObjSubMenu.Add(new SubMenu { MainMenuID = 1, SubMenuItem = "Departemen", SubMenuURL = "#" });
            ObjSubMenu.Add(new SubMenu { MainMenuID = 1, SubMenuItem = "Supplier", SubMenuURL = "#" });

            return ObjSubMenu;
        }
    }
}
