using Model;
using Model.LINQmodels;
using RealBranch.Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace RealBranch.Controllers
{
    public class WebController : ApiController
    {
        EFAccountRepository repository = new EFAccountRepository();
        ARContext db = new ARContext();
        AccountController acc = new AccountController();
        //public  IEnumerable<UserProfileModel> GetAllReservations(AppUser targetuser)
        //{
    
        //    return EFAccountRepository.AnotherOneUserProfile(targetuser);
        //}


        public IHttpActionResult Get()
        {
            return Ok("It works!");
        }
        // GET api/Sample/{id}
        public IHttpActionResult Get(string id)
        {
            return Ok("It works! Your id is " + id);
        }

        //// POST api/Sample
        //public void Post([FromBody]string value)
        //{
        //    throw new NotSupportedException();
        //}

        //// PUT api/Sample/{id}
        //public IHttpActionResult Put(string id, [FromBody]string value)
        //{
        //    return Ok("It works! Your id is " + id);
        //}

        // DELETE api/Sample/{id}
        [HttpGet]
        [Route("api/Web/Delete")]
        public IHttpActionResult Delete()
        {
            acc.UserProfile();
            
            return Ok("That is it!");
        }

        //[HttpGet]
        //[Route("api/Sample/Custom")]
        //public IHttpActionResult Custom()
        //{
        //    // sample custom action method using attribute-based routing
        //    // TODO: my code here
        //    throw new NotImplementedException();
        //}
    }
}