using MongoDB.Bson;
using RealEstate.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RealEstate.Utilities;

namespace RealEstate.Rentals
{
    public class RentalsController : Controller
    {

        public readonly RealEstateContext Context = new RealEstateContext();

        // GET: Rentals
        public async Task<ActionResult> Index()
        {
            var filter = new BsonDocument();
            var rentals = await Context.Rentals.FindAsync<Rental>(filter).ToListAsync();            
            return View(rentals);
        }

        // GET: Rentals/Post
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post(PostRental postRental)
        {
            var rental = new Rental(postRental);
            await Context.Rentals.InsertOneAsync(rental);
            return RedirectToAction("Index");
        }

        // GET: Rentals/AdjustPrice/5
        public async Task<ActionResult> AdjustPrice(string id)
        {
            var rental = await GetRental(id);
            return View(rental);
        }

        [HttpPost]
        public async Task<ActionResult> AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var rental = await GetRental(id);
            rental.AdjustPrice(adjustPrice);
            var filter = new BsonDocument("_id", new ObjectId(id));
            await Context.Rentals.ReplaceOneAsync(filter, rental);
            return RedirectToAction("Index");
        }

        private async Task<Rental> GetRental(string id)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            return await Context.Rentals.FindAsync<Rental>(filter).FindOneAsync();
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rentals/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
