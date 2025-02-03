using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Urun.Models;
using PagedList.Mvc;

namespace Urun.Controllers
{
    public class UrunController : Controller
    {
        UrunlerEntities entities = new UrunlerEntities();
        // GET: Urun
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            List<Urunler> list = entities.Urunler.ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        public ActionResult AddWriterPartial()
        {
            return PartialView("_AddProductPartialView");
        }


        [HttpPost]
        public JsonResult AddProduct(Urunler urun)
        {
            try
            {
                entities.Urunler.Add(urun);
                entities.SaveChanges();

                return Json(new { success = true, message = "Ürün başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
               
                    var urun = entities.Urunler.Find(id);
                    if (urun == null)
                    {
                        return Json(new { success = false, message = "Ürün bulunamadı." });
                    }

                entities.Urunler.Remove(urun);
                entities.SaveChanges();
               

                return Json(new { success = true, message = "Ürün başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }





        [HttpGet]
        public ActionResult UpdateProduct(int id )
        {
            var product = entities.Urunler.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.Product = product;
            return PartialView();
        }


        [HttpPost]
        public JsonResult UpdateProduct(Urunler urun)
        {
            var updateProduct = entities.Urunler.Where(m => m.Id == urun.Id).FirstOrDefault();

            if (updateProduct != null)
            {
                updateProduct.UrunAd = urun.UrunAd;
                updateProduct.UrunBarkod = urun.UrunBarkod;
                entities.SaveChanges();
                return Json(new { success = true, message = "Ürün başarıyla güncellendi!" });
            }
            return Json(new { success = false, message = "Ürün bulunamadı!" });
        }

        //public JsonResult GetById(int id)
        //{
        //    var urun = entities.Urunler.Where(u => u.Id == id).Select(u => new
        //    {
        //        u.Id,
        //        u.UrunAd,
        //        u.UrunBarkod
        //    }).FirstOrDefault();

        //    if (urun == null)
        //    {
        //        return Json(new { success = false, message = "Ürün bulunamadı." }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(urun, JsonRequestBehavior.AllowGet);
        //}



    }
}