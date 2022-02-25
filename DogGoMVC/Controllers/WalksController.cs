using DogGoMVC.Models;
using DogGoMVC.Models.ViewModels;
using DogGoMVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DogGoMVC.Controllers
{
    public class WalksController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IWalksRepository _walksRepo;

        public WalksController(IWalksRepository walksRepo, IWalkerRepository walkerRepo, IDogRepository dogRepo, INeighborhoodRepository neighborhoodRepo, IOwnerRepository ownerRepo)
       {    

        
            _ownerRepo = ownerRepo;
            _dogRepo = dogRepo;
            _walkerRepo = walkerRepo;
            _neighborhoodRepo = neighborhoodRepo;
            _walksRepo = walksRepo;

        }
        // GET: WalksController
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();

            return View(walks);
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            List<Dog> dogs = _dogRepo.GetAllDogs();
            List<Walker> walkers = _walkerRepo.GetAllWalkers();


            WalkFormViewModel vm = new WalkFormViewModel()
            {
                Walks = new Walks(),
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        // POST: OwnerController/Create
        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walks walk)
        {
            try
            {
                // Get the values from multiselect in create form before creating new walks
                // They will be in Key/Value pairs but I just want the value since it's iterable
                // The iterable variable dogIds will have each value that was selected
                var dogIds = Request.Form["Walks.dogId"];
                var walkerId = Request.Form["Walks.walkerId"];
                var duration = Request.Form["Walks.duration"];
                var id = Request.Form["Walks.id"];



                // Iterate over the ids that were selected in the form and create a new record
                // for each of the values that were selected in the multiselect
                foreach (var dogId in dogIds)
                {
                    // They will be string so convert to int then
                    // let the repo take care of the work
                    walk.DogId = Int32.Parse(dogId);
                    _walksRepo.AddWalks(walk);
                }
                walk.Duration = Int32.Parse(duration);
                walk.WalkerId = Int32.Parse(walkerId);
                walk.Id = Int32.Parse(id);

                _walksRepo.AddWalks(walk);

                return RedirectToAction("Index");
            }
            catch
            {
                // It's important to replicate the Get functionality into the catch of the Post
                // I was experiencing errors here as well as the OwnersController and couldn't
                // figure out why... turns out this did the trick.. maybe not best solution
                // but it doesn't break now at least.
                WalkFormViewModel vm = new WalkFormViewModel()
                {
                    Walks = new Walks(),
                    Walkers = _walkerRepo.GetAllWalkers(),
                    Dogs = _dogRepo.GetAllDogs()
                };
                return View(vm);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
