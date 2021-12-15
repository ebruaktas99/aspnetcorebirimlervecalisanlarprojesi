using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreProjeRevize.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProjeRevize.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
       
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        public async Task<IActionResult>GirisYap(Admin p)
        {

            var bilgiler = c.Admins.FirstOrDefault(x => x.Kullanici == p.Kullanici && x.Sifre == p.Sifre);

            if(bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, p.Kullanici) //talep :birincisi türü ikincisi value değeri
                };

                var useridentity = new ClaimsIdentity(claims, "Login"); //talebin kimliğiyle ilişkilendirdik.
                                                                        //claims yukarıda tanımladığımız talepten gelen değer, login de autoriden gelen değer

                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);//şifreli bir cookie oluşturur. //asenkron ifadeleri kullanabilmek için başına await eklenir.

                return RedirectToAction("Index", "Personelim");
            }
            return View();
        }
    }

}
