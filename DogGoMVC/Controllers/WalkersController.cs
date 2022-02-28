using DogGoMVC.Models;
using DogGoMVC.Models.ViewModels;
using DogGoMVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace DogGoMVC.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IWalksRepository _walksRepo;

        public WalkersController(
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository,
            IWalkerRepository walkerRepository,
            INeighborhoodRepository neighborhoodRepository,
            IWalksRepository walksRepository)

        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
            _walksRepo = walksRepository;

        }
        // GET: HomeController
        // GET: Walkers

        public ActionResult Index()
        {
            int currentUserId = GetCurrentUserId();

            if (currentUserId != 0)
            {
                Owner currentUser = _ownerRepo.GetOwnerById(currentUserId);
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(currentUser.NeighborhoodId);
                return View(walkers);
            }
            else
            {
                List<Walker> walkers = _walkerRepo.GetAllWalkers();
                return View(walkers);
            }
        }

        // GET: HomeController/Details/5
        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetWalksByWalkerId(walker.Id);
            List <Neighborhood> neighborhood = _neighborhoodRepo.GetAll();
            List<Owner> owner = _ownerRepo.GetAllOwners();
            List <Dog> dogs = _dogRepo.GetDogsByOwnerId(walker.Id);



            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks,
                Neighborhood = neighborhood,
                Owner = owner,
                Dog = dogs
            };

            return View(vm);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
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

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
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
        public int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
