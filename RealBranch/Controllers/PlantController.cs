
using Model;
using RealBranch.Controllers;
using RealBranch.Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AugmentedReality.Controllers
{
    public class PlantController : BaseController
    {
        EFPlantRepository repository = new EFPlantRepository();
        // GET: Plant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPlantsBySpeciesId(int Id)
        {
            return View(repository.GetPlantsBySpeciesId(Id));
        }
        public ActionResult PlantInfo(int Id)
        {

            return View(repository.PlantInfo(Id));
        }
    }
}