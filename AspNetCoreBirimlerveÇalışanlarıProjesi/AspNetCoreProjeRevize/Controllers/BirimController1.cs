using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreProjeRevize.Models; //contexti ekleyebilmek için
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProjeRevize.Controllers
{
    public class BirimController1 : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.Birims.ToList();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YeniBirim(Birim d) // birim sınıfından d parametresi kullanır
        {
            c.Birims.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimSil(int id)
        {
            var dep = c.Birims.Find(id);
            c.Birims.Remove(dep);
            c.SaveChanges();

            return RedirectToAction("Index");

        }


        public IActionResult BirimGetir(int id)
        {
            var depart = c.Birims.Find(id);

            return View("BirimGetir", depart); //açılan birimgetir sayfasında textbox da departmanın adı yazsın diye, id zaten yazıyor çünkü parametre olarak gönderdik.
        }

        public IActionResult BirimGuncelle(Birim d)
        {
            var dep = c.Birims.Find(d.BirimID);
            dep.BirimAd= d.BirimAd;
            c.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult BirimDetay( int id)
        {
            var degerler = c.Personels.Where(x => x.BirimID == id).ToList();
            var brmad = c.Birims.Where( x=>x.BirimID ==id).Select(y=>y.BirimAd).FirstOrDefault(); //Çalışanların birimleri yazar

            ViewBag.brm = brmad; //viewbaga brmadi attık, birimdetay da çağırıyoruz.
            
            return View(degerler);
        }
    }
}

