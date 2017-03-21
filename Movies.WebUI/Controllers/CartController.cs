using Movies.Domain.Abstract;
using Movies.Domain.Entities;
using Movies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Movies.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IMovieRepository repository = null;
        public CartController(IMovieRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult Add(Cart cart, string id, string returnUrl)
        {
            Movie mv = repository.Movies.FirstOrDefault(m => m.ID == id);
            if (mv != null)
            {
                cart.AddLine(mv, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult Remove(Cart cart, string id, string returnUrl)
        {
            Movie mv = repository.Movies.FirstOrDefault(m => m.ID == id);
            if (mv != null)
            {
                cart.RemoveLine(mv);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public JsonResult Badge(Cart cart)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";
            IDictionary<string, int> obj = new Dictionary<string, int>
            {
                { "badge", cart.CalcTotalCount()}
            };

            json.Data = obj;

            return json;
        }

        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                cart.Clear();

                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}