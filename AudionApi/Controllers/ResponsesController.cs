using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AudionApi.Models;

namespace AudionApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ResponsesController : Controller
  {
    private readonly AudionApiContext _db;

    public ResponsesController(AudionApiContext db)
    {
      _db = db;
    } 

    // GET /api/responses
    [HttpGet]
    public ActionResult<ICollection<Response>> Get(int questionId)
    {
      var query = _db.Responses.AsQueryable();

      if (questionId != 0)
      {
        query = query.Where(entry => entry.QuestionId == questionId);
      }

      return query.ToList();
    }

    // GET api/Responses/5
    [HttpGet("{id}")]
    public ActionResult<Response> Get(int questionId, int id)
    {
      var thisResponse = _db.Responses.FirstOrDefault(entry => entry.ResponseId == id);
      return thisResponse;
    }

    // POST api/Responses
    [HttpPost]
    public void Post(int questionId, [FromBody] Response response)
    {
      //response.QuestionId = questionId;
      response.Timestamp = DateTime.Now;
      _db.Responses.Add(response);
      _db.SaveChanges();
    }

    // PUT api/Responses/5
    [HttpPut("{id}")]
    public void Put(int id, int questionId, [FromBody] Response response)
    {
      response.ResponseId = id;
      //response.QuestionId = questionId;
      response.Timestamp = DateTime.Now;
      _db.Entry(response).State = EntityState.Modified;
      _db.SaveChanges();
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var thisResponse = _db.Responses.FirstOrDefault(entry => entry.ResponseId == id);
      _db.Responses.Remove(thisResponse);
      _db.SaveChanges();
    }


    // public ActionResult Index()
    // {
    //   return View();
    // }
    
    // public ActionResult Create()
    // {
    //   ViewBag.QuestionId = new SelectList(_db.Questions, "QuestionId", "Location");
    //   return View();
    // }

    // [HttpPost]
    // public ActionResult Create(Response response)
    // {
    //   _db.Responses.Add(response);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
  }
}