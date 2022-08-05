using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SortingMVC.Context;
using SortingMVC.Entities;
using SortingMVC.Models;

namespace SortingMVC.Controllers
{
    public class SortingController : Controller
    {
        SortingDbContext db;

        public SortingController(SortingDbContext _db)
        {
            this.db = _db;
        }


        // GET: SortingController
        public ActionResult Index(string sortOrder, string searchsort)
        {
            
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = String.IsNullOrEmpty(sortOrder) ? "address_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            

            ViewData["CurrentFilter"] = searchsort;
            var sorting = from s in db.SortDatas select s;

            if (!String.IsNullOrEmpty(searchsort))
            {
                sorting = sorting.Where(s => s.Name.Contains(searchsort));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sorting = sorting.OrderByDescending(s => s.Name);
                    break;
                case "address_desc":
                    sorting = sorting.OrderByDescending(s => s.Address);
                    break;
                case "date_desc":
                    sorting = sorting.OrderByDescending(s => s.DateJoined);
                    break;
                default:
                    sorting = sorting.OrderBy(s => s.Name);
                    break;

            }
            
            return View(sorting);
            
        }

        

        // GET: SortingController/Details/5
        public ActionResult Details(SortDataModel sd)
        {
            var sdm = db.SortDatas.Where(i => i.Id == sd.Id).FirstOrDefault();
            return View(sdm);
        }

        // GET: SortingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SortingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SortDataModel sdm)
        {
            try
            {
                var sd = new SortData()
                {
                    Id = sdm.Id,
                    Name = sdm.Name,
                    Address = sdm.Address,
                    DateJoined = sdm.DateJoined,
                };

                db.SortDatas.Add(sd);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SortingController/Edit/5
        public ActionResult Edit(SortDataModel sd)
        {
            var sdm = db.SortDatas.Where(i => i.Id == sd.Id).FirstOrDefault();
            return View(sdm);
        }

        // POST: SortingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SortData sd)
        {
            try
            {
                var sdm = db.SortDatas.Where(i => i.Id == sd.Id).FirstOrDefault();
                sdm.Name = sd.Name;
                sdm.Address = sd.Address;
                sdm.DateJoined = sd.DateJoined;
                db.SortDatas.Update(sdm);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SortingController/Delete/5
        public ActionResult Delete(SortDataModel sd)
        {
            var sdm = db.SortDatas.Where(i => i.Id == sd.Id).FirstOrDefault();
            return View(sdm);
        }

        // POST: SortingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SortData sd)
        {
            try
            {
                var sdm = db.SortDatas.Where(i => i.Id == sd.Id).FirstOrDefault();
                if(sdm != null)
                {
                    db.SortDatas.Remove(sdm);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
