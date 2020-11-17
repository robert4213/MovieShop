using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopWeb.Controllers.User
{
    public class UserController
    {
        [HttpPost]   
        public void Create(User user)
        {
            return;
        }

        public IActivator Details(int userid)
        {
            return View();
        }
    }
}