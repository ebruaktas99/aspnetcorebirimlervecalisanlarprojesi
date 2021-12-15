using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreProjeRevize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProjeRevize.Controllers
{
    public class PersonelimController : Controller
    {
        Context c = new Context();
        
        [Authorize] //startup da authorize işlemlerini tanımladık.Böylelikle hangi sayadan başlanacağın belirledik. Giriş sayfasıyla başlamış olacak.
        public IActionResult Index()
        {
            var degerler = c.Personels.Include(x => x.Birim).ToList(); //birim adı direkt gelsin
            return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniPersonel()
        {
            //birim seçilsin diye dropdownlist
            List<SelectListItem> degerler = (from x in c.Birims.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }).ToList() ;

            ViewBag.dgr = degerler; //dgr ye degerleri atadık.
            return View();
        }

        [HttpPost]
        public IActionResult YeniPersonel(Personel p)

        { //parametreden göndermiş olduğumuz değeri per değişkenine atamış olur.
            var per = c.Birims.Where(x => x.BirimID ==p.Birim.BirimID).FirstOrDefault();
            p.Birim = per;
            //yukarıdaki iki satır ile dropdowndan seçileni almış oluyoruz.
            c.Personels.Add((p));
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult PersonelSil(int id)
        {
            var per = c.Personels.Find(id);
            c.Personels.Remove(per);
            c.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult PersonelGetir(int id)
        {
              
            //birim seçilsin diye dropdownlist
            List<SelectListItem> degerler = (from x in c.Birims.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }).ToList();
            
            ViewBag.dgr = degerler; //dgr ye degerleri atadık.


            var per = c.Personels.Find(id);
            return View("PersonelGetir", per); //açılan birimgetir sayfasında textbox da departmanın adı yazsın diye, id zaten yazıyor çünkü parametre olarak gönderdik.
        }

        public IActionResult PersonelGuncelle(Personel p)
        {
            
                var per = c.Personels.Find(p.PersonelID);
                per.Ad = p.Ad;// adını güncelledik
                per.Soyad = p.Soyad; //soyadını güncelledik
                per.Sehir = p.Sehir; //Sehrini Güncelledik
                var per1 = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
                p.Birim = per1;
                per.Birim = p.Birim;
                c.SaveChanges();
                return RedirectToAction("Index");
           
        }

        

    }
}
