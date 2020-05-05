using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealBranch.Repositories
{
    public class EFPlantRepository
    {

        ARContext db = new ARContext();
        internal List<Plant> GetPlantsBySpeciesId(int id)
        {


            var courses = db.Plants
        .Where(c => c.SpeciestId == id)
        .AsNoTracking();
            return courses.ToList();
        }

        internal object PlantInfo(int id)
        {

            Plant plant
                   = db.Plants.Where(x => x.PlantId==id).FirstOrDefault();

            return plant;
        }
    }
}
