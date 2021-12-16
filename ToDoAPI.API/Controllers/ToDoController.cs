using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoAPI.DATA.EF;
using ToDoAPI.API.Models;
using System.Web.Http.Cors;


namespace ToDoAPI.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ToDoController : ApiController
    {
        ToDoEntities db = new ToDoEntities();

        public IHttpActionResult GetToDoItems()
        {
            List<ToDoViewModel> Items = db.TodoItems.Include("Category").Select(i => new ToDoViewModel()
            {
                ID = i.ID,
                Action = i.Action,
                Done = i.Done,
                CategoryId = i.CategoryId,
                Category = new CategoryViewModel()
                {
                    ID = i.Category.ID,
                    Name = i.Category.Name,
                    Description = i.Category.Description
                }
            }).ToList<ToDoViewModel>();

            if (Items.Count == 0)
            {
                return NotFound();//404 error
            }
            return Ok(Items);
        }
    }
}
