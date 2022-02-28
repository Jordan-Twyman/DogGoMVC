using DogGoMVC.Models;
using DogGoMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DogGoMVC.Controllers
{
    public class DogsController : Controller
    {

        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public DogsController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }
        // GET: DogController
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);
            try
            {
                



                return View(dogs);
            }
            catch (Exception ex)
            {

                return View(dogs);
            }
        }

        // GET: HomeController/Details/5
        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }
        // GET: DogController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        // POST: Dogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id 
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Edit/5
        // GET: Dogs/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            int currentUserId = GetCurrentUserId();

            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null || dog.OwnerId != currentUserId)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: DogController/Edit/5
        // POST: Dogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Delete/5
        // GET: Dogs/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            int currentUserId = GetCurrentUserId();

            Dog dog = _dogRepo.GetDogById(id);
            if (currentUserId != dog.OwnerId)
            {

                return NotFound();
                    }
            
            
                return View(dog);
            
        }


        // POST: DogController/Delete/5
        // POST: Dogs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        public int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}

