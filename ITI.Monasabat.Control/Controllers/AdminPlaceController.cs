using ITI.Monasabat.Control.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Monasapat.Models;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ITI.Monasabat.Control.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPlaceController : Controller
    {
        MonasabatContext Db = new MonasabatContext();
        public IActionResult Index()
        {

            var Query = Db.Places?.ToList();
            return View("AllPlaces", Query);
        }

        public IActionResult GetID(int id)
        {
            if (id == null || Db == null)
            {
                return NotFound();
            }



            var places = Db.Places?.Where(i => i.ID == id).FirstOrDefault();
            if (places != null)
            {
                ViewBag.Name = $"Details Of | {places.Name}";
                return View("Details", places);
            }
            else
            {

                var mylist = Db.Places.Select(x => x).ToList();

                return View("AllPlaces", mylist);

            } }

        [HttpGet]

        public IActionResult Add()
        {

            ViewBag.Cities = Db.Cities.Select(i => new SelectListItem(i.Name, i.ID.ToString()));
            ViewBag.Owners = Db.PlaceOwners.Select(i => new SelectListItem(i.Name, i.ID.ToString()));




            return View();
        }
        [HttpPost]

        public IActionResult Add(AddViewModel place)

        {
             
            //bool IsValidExtension = true;
            //Regex regex = new Regex("([^\\w](.jpe?g|.png|.gif|.bmp|.jfif))$)");
            if (place.Images != null)
            {

                foreach (IFormFile item in place?.Images)
                {

                    if (!Regex.IsMatch(item.FileName.Trim(), "([a-zA-Z0-9\\s_\\\\.\\-:])+(.png|.jpg|.gif)$"))
                    {
                        //IsValidExtension = false;
                        ModelState.AddModelError("Images", "Upload Valid Extension For Ur Image");
                        ViewBag.Cities = Db.Cities.Select(i => new SelectListItem(i.Name, i.ID.ToString()));
                        ViewBag.Owners = Db.PlaceOwners.Select(i => new SelectListItem(i.Name, i.ID.ToString()));

                        return View();
                    }
                }
            }

            if (ModelState.IsValid )
            {

                List<PlaceImage> IMages = new List<PlaceImage>();

                foreach (IFormFile file in place.Images)
                {

                    string NewNameOfFile = Guid.NewGuid().ToString() + file.FileName;
                    FileStream str = new FileStream
                        (
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "placeimg", NewNameOfFile)
                        , FileMode.OpenOrCreate, FileAccess.ReadWrite
                        );
                    IMages.Add(new PlaceImage()
                    {
                        Path = NewNameOfFile
                    });
                    file.CopyTo(str);
                    str.Position = 0;
                }
                Place place1 = new Place();
                place1.Name = place.Name;
                place1.Type = place.Type;
                place1.Price = place.Price;
                place1.CityID = place.CityID;
                place1.PlaceOwnerID = place.PlaceOwnerID;
                place1.PlaceImages = IMages;
                Db.Places.Add(place1);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cities = Db.Cities.Select(i => new SelectListItem(i.Name, i.ID.ToString()));
                ViewBag.Owners = Db.PlaceOwners.Select(i => new SelectListItem(i.Name, i.ID.ToString()));

                return View();
            }

        }

        public IActionResult Delete(int id)
        {
            var places = Db.Places?.Where(i => i.ID == id).FirstOrDefault();
            Db.Places.Remove(places);
            Db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var places = Db.Places?.Where(i => i.ID == id).FirstOrDefault();
            ViewBag.Title = places?.Name;
            ViewBag.PlaceName = places?.Type;
            ViewBag.PlacePrice = places?.Price;

            ViewBag.cities = Db.Cities?.ToList();
            ViewBag.placeowners = Db.PlaceOwners?.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Edit(Place place)
        {
            
            //bool IsValidExtension = true;
            //Regex regex = new Regex("([^\\s]+(\\.(?i)(jpe?g|png|gif|bmp))$)");
            //if (place.Images != null)
            //{

            //    foreach (IFormFile item in place?.Images)
            //    {

            //        if (regex.IsMatch(item.Name) == false)
            //        {
            //            IsValidExtension = false;
            //        }
            //    }
            //}
            
            if (ModelState.IsValid || place.PlaceOwnerID == 0 || place.CityID == 0 || place.Images == null || place.Price < 0 )
            {



                var places = Db.Places?.Where(i => i.ID == place.ID).FirstOrDefault();
                ViewBag.Title = places?.Name;
                ViewBag.PlaceName = places?.Type;
                ViewBag.PlacePrice = places?.Price;
                ViewBag.cities = Db.Cities.ToList();
                ViewBag.placeowners = Db.PlaceOwners.ToList();
                ModelState.AddModelError("", "you must entre a picture");
                

                return View();


            }
            else
            {
                



                Place placeedit = Db.Places.Where(i => i.ID == place.ID).FirstOrDefault();

                List<PlaceImage> IMages = new List<PlaceImage>();
                foreach (IFormFile file in place?.Images)
                {
                    string NewNameOfile = Guid.NewGuid().ToString() + file.FileName;
                    FileStream str = new FileStream
                        (
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "placeimg", NewNameOfile)
                            , FileMode.OpenOrCreate, FileAccess.ReadWrite
                        );
                    IMages.Add(new PlaceImage()
                    {
                        Path = NewNameOfile
                    });
                    file.CopyTo(str);
                    str.Position = 0;
                }

                placeedit.Name = place.Name;
                placeedit.Type = place.Type;
                placeedit.PlaceOwnerID = place.PlaceOwnerID;
                placeedit.CityID = place.CityID;
                placeedit.Price = place.Price;
                placeedit.PlaceImages = IMages;
               


                Db.SaveChanges();

                return RedirectToAction("Index");

                
            }

            


        }
    }
}
