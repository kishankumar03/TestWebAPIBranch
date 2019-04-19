using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestWebAPIBranch.Context;
using TestWebAPIBranch.Models;
namespace TestWebAPIBranch.Controllers
{
    public class ValuesController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        // GET api/values
        [ResponseType(typeof(List<UserLogin>))]
        [HttpGet]
        [Route("api/GetUserLogins")]
        public IHttpActionResult Get()
        {
            try
            {
                var result = from users in db.UserLogins
                             select new
                             {
                                 users.Id,
                                 users.password,
                                 users.user_name
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
                throw;
            }
        }

        // GET api/values/5
        [ResponseType(typeof(UserLogin))]
        [HttpGet]
        [Route("api/GetUserLogins/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var user = db.UserLogins.Find(id);
                var result = new
                {
                    user.Id,
                    user.user_name
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/values
        [ResponseType(typeof(UserLogin))]
        [Route("api/AddUserLogin")]
        [HttpPost]
        public IHttpActionResult Post(UserLogin value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Entry(value).State = System.Data.Entity.EntityState.Added;
            db.UserLogins.Add(value);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = value.Id }, value);
        }

        // PUT api/values/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/UpdateUserLogin/{id:int}")]
        public IHttpActionResult Put(int id, UserLogin value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != value.Id)
                return BadRequest();
            db.Entry(value).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(db.UserLogins.Count(e => e.Id == id) > 0))
                    return NotFound();
                else
                    throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/values/5
        [ResponseType(typeof(UserLogin))]
        [HttpDelete]
        [Route("api/DeleteUserLogin/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            UserLogin userLogin = db.UserLogins.Find(id);
            if (userLogin == null)
                return NotFound();
            db.UserLogins.Remove(userLogin);
            db.SaveChanges();
            return Ok(userLogin);
        }
    }
}
