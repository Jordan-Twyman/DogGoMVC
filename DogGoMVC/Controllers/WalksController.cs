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
        public ActionResult Create(WalkFormViewModel vm)           // Create(Walks walks)
        {
            try
            {
                //var date = Request.Form["Walks.Date"];

                //var dogIds = Request.Form["Walks.DogId"];
                //var walkerId = Request.Form["Walks.WalkerId"];
                //var duration = Request.Form["Walks.Duration"];

                //walk.Date = DateTime.Parse(date);
                //walk.Duration = Int32.Parse(duration);
                //walk.WalkerId = Int32.Parse(walkerId);


                //foreach (var dogId in dogIds)
                //{

                //    walks.DogId = Int32.Parse(dogId);
                //    _walksRepo.AddWalks(walks);
                //}


                //_walksRepo.AddWalks(walks);


                //This is the working code for the View Model*************************************

                var dogIds = Request.Form["Walks.DogId"];


                foreach (string id in dogIds)
                {
                    
                    vm.Walks.DogId = Int32.Parse(id);
                    _walksRepo.AddWalks(vm.Walks);
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex) 
            {
                // It's important to replicate the Get functionality into the catch of the Post
                // I was experiencing errors here as well as the OwnersController and couldn't
                // figure out why... turns out this did the trick.. maybe not best solution
                // but it doesn't break now at least.
                //WalkFormViewModel vm = new WalkFormViewModel()
                //{
                //    Walks = new Walks(),
                //    Walkers = _walkerRepo.GetAllWalkers(),
                //    Dogs = _dogRepo.GetAllDogs()
                //};
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
