using kodusorClient.kodusorServis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kodusorClient.Controllers
{
    public class ProfilController : Controller
    {
        KodusorServisClient servis;

        // GET: Profil
        public ActionResult Index()
        {
            if (Session["kullaniciID"] != null)
            {
                servis = new KodusorServisClient();
                int kulID = Convert.ToInt32(Session["kullaniciID"]);
                var kullanici = servis.KullaniciBilgileriniGetir(kulID);
                return View(kullanici);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SoruSor(int? id)
        {
            return View(id);
        }

        public JsonResult Cikis()
        {
            Session["kullaniciID"] = null;
            return Json("+");
        }
    }
}