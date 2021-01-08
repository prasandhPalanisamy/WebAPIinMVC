using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<MVCEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<MVCEmployeeModel>>().Result;
            return View(empList);
        }
        public ActionResult AddOrEdit(int id)
        {
            if (id == 0)
            {
                return View(new MVCEmployeeModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCEmployeeModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MVCEmployeeModel emp)
        {
            if (emp.EmployeeId == 0)
            {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", emp).Result;
            
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employee/"+emp.EmployeeId,emp).Result;
            }
                return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employee/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}