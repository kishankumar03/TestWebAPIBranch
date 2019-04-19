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

namespace TestWebAPIBranch
{
    public class DefaultController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET api/<controller>
        [HttpGet]
        [Route("api/GetUserInfo")]
        public IHttpActionResult Get()
        {
            try
            {
                var result = from users in db.UserInfos
                             select new
                             {
                                 users.firstName,
                                 users.lastName,
                                 users.userId,
                                 users.gender,
                                 users.UserLogin.user_name
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/GetUserInfo/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var user = db.UserInfos.Find(id);
                var result = new
                {
                    user.firstName,
                    user.lastName,
                    user.userId,
                    user.gender,
                    user.UserLogin.user_name
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/<controller>
        [ResponseType(typeof(UserInfo))]
        [Route("api/AddUserInfo")]
        [HttpPost]
        public IHttpActionResult Post(UserInfo value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUserdetail = db.UserLogins.Find(value.userId);
            value.UserLogin = existingUserdetail;
            value.userId = existingUserdetail.Id;
            db.UserInfos.Add(value);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = value.userId }, value);
        }

        // PUT api/<controller>/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/UpdateUserInfo/{id:int}")]
        public IHttpActionResult Put(int id, UserInfo userInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != userInfo.userId)
                return BadRequest();
            db.Entry(userInfo).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(db.UserInfos.Count(e => e.userId == id) > 0))
                    return NotFound();
                else
                    throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/<controller>/5
        [ResponseType(typeof(UserInfo))]
        [HttpDelete]
        [Route("api/DeleteUserInfo/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            UserInfo userInfo = db.UserInfos.Find(id);
            if (userInfo == null)
                return NotFound();
            db.UserInfos.Remove(userInfo);
            db.SaveChanges();
            return Ok(userInfo);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}