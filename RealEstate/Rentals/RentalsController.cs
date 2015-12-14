using MongoDB.Bson;
using MongoDB.Driver;
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

        //Replacing
        [HttpPost]
        public async Task<ActionResult> AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var rental = await GetRental(id);
            rental.AdjustPrice(adjustPrice);
            var filter = new BsonDocument("_id", new ObjectId(id));
            await Context.Rentals.ReplaceOneAsync(filter, rental);
            return RedirectToAction("Index");
        }

        //Modification
        [HttpPost]
        public async Task<ActionResult> AdjustPrice_RemoveThisBitToMakeMeActive(string id, AdjustPrice adjustPrice)
        {
            var rental = await GetRental(id);
            var adjustment = new PriceAdjustment(adjustPrice, rental.Price);
            var filter = Builders<Rental>.Filter.Eq(r => r.Id, id);
            var update = Builders<Rental>
                .Update.Push(r => r.Adjustments, adjustment)
                .Set(r => r.Price, adjustPrice.NewPrice);
            await Context.Rentals.UpdateOneAsync(filter, update);            
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
        public async Task<ActionResult> Edit(string id)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            var rental = await Context.Rentals.Find(filter).FirstOrDefaultAsync();
            var postRental = new PostRental
            {
                //TODO: Edit is not ready yet
            };
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, PostRental postRental)
        {
            try
            {

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
