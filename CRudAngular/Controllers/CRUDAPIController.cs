using CRudAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace CRudAngular.Controllers
{
    [System.Web.Http.RoutePrefix("Api/Employee")]
    public class CRUDAPIController : Controller
    {
        CrudAngularEntities objEntity = new CrudAngularEntities();

        // GET: CRUDAPI
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("AllEmployeeDetails")]
        public IQueryable<EmployeeDetail>GetEmployee()
        {

            try
            {
                return objEntity.EmployeeDetails;
            }
            catch(Exception)
            {
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmployeeDetailsById(string employeeId)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int ID = Convert.ToInt32(employeeId);
            try
            {
                objEmp = objEntity.EmployeeDetails.Find(ID);
                if(objEmp == null)
                {
                    return NotFound();
                }
            }
            catch(Exception)
            {
                throw;
            }
            return ok(objEmp);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmaployee(EmployeeDetail data)
        {
          if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.EmployeeDetails.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return ok(data);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmaployeeMaster(EmployeeDetail employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmployeeDetail objEmp = new EmployeeDetail();
                objEmp = objEntity.EmployeeDetails.Find(employee.Empld);
                if(objEmp !=null)
                {
                    objEmp.EmpName = employee.EmpName;
                    objEmp.Address = employee.Address;
                    objEmp.EmailId = employee.EmailId;
                    objEmp.DateOfBirth = employee.DateOfBirth;
                    objEmp.Gender = employee.Gender;
                    objEmp.pinCode = employee.pinCode;
                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return ok(employee);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmaployeeDelete(int id)
        {
            EmployeeDetail employee = objEntity.EmployeeDetails.Find(id);
            if(employee == null)
            {
                return NotFound();

            }
            objEntity.EmployeeDetails.Remove(employee);
            objEntity.SaveChanges();

            return ok(employee);
        }

        private IHttpActionResult BadRequest(ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        private IHttpActionResult ok(EmployeeDetail objEmp)
        {
            throw new NotImplementedException();
        }

        private IHttpActionResult NotFound()
        {
            throw new NotImplementedException();
        }
    }
}