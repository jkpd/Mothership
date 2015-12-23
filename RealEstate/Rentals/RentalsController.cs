using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
        public async Task<ActionResult> Index(RentalFilter filter)
        {
            IEnumerable<Rental> rentals = null;
            if (filter == null)
            {
                var bsonFilter = new BsonDocument();
                //Simple test for CoreControl
                //var generic = await Context.GenericData_Alerts.FindAsync<Models.GenericData_Alerts>(filter).ToListAsync();
                rentals = await Context.Rentals.FindAsync<Rental>(bsonFilter).ToListAsync();
            }
            else
                rentals = await FilterRentals(filter);

            var model = new RentalsList
            {
                RentalFilter = filter,
                Rentals = rentals
            };
            return View(model);
        }

        /// <summary>
        /// Filter Rentals
        /// USING MongoDB.Driver.Linq
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Rental>> FilterRentals(RentalFilter filter)
        {            
            var collection = Context.Rentals.AsQueryable();
            IMongoQueryable<Rental> query = collection;
            if (filter.PriceLimit != null)
                query = collection.Where(d => d.Price <= filter.PriceLimit);
            if (filter.SortBy == RentalFilter.SortOptions.Price)
                query = query.OrderBy(d => d.Price);
            else
                query = query.OrderBy(d => d.Address);
            return await query.ToListAsync();
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

        /// <summary>
        /// Replacing using FilterDefinition 
        /// USING REPLACE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adjustPrice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var rental = await GetRental(id);
            rental.AdjustPrice(adjustPrice);
            var filter = new BsonDocument("_id", new ObjectId(id));
            await Context.Rentals.ReplaceOneAsync(filter, rental);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Modification using Builders (Currently inactive)
        /// USING UPDATE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adjustPrice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AdjustPrice_RemoveThisBitToMakeMeActive(string id, AdjustPrice adjustPrice)
        {
            var rental = await GetRental(id);
            var adjustment = new PriceAdjustment(adjustPrice, rental.Price);
            var filter = Builders<Rental>.Filter.Eq(r => r.Id, id);
            var update = Builders<Rental>
                .Update
                .Push(r => r.Adjustments, adjustment)
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
