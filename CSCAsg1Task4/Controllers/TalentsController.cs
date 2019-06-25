using CSCAsg1Task4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSCAsg1Task4.Controllers
{
    public class TalentsController : ApiController
    {
        static readonly ITalentRepository repository = new TalentRepository();
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [RequireHttps]
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [HttpGet]
        [RequireHttps]
        [Route("api/talents/{id:int:min(2)}", Name = "getTalentById")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //PUT, POST and DELETE
        [HttpPost]
        [RequireHttps]
        [Route("api/talents")]
        public HttpResponseMessage PostTalent(Talent item)
        {
            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Talent>(HttpStatusCode.Created, item);

                // Generate a link to the new talent and set the Location header in the response.

                string uri = Url.Link("getTalentById", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [RequireHttps]
        [Route("api/talents/{id:int}")]
        public void PutTalent(int id, Talent talent)
        {
            talent.Id = id;
            if (!repository.Update(talent))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [RequireHttps]
        [Route("api/talents/{id:int}")]
        public void DeleteTalent(int id)
        {
            repository.Remove(id);
        }
    }
}
