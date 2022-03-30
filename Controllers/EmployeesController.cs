using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {

        }

        [HttpGet("TestCORS")]
        public ActionResult TestCors()
        {
            return Ok("Test CORS berhasil");
        }
    }

    //public class EmployeesController : ControllerBase
    //{
    //    private readonly EmployeeRepository employeeRepository;

    //    public EmployeesController(EmployeeRepository employeeRepository)
    //    {
    //        this.employeeRepository = employeeRepository;
    //    }


    //    [HttpGet]
    //    public ActionResult Get()
    //    {
    //        int count = employeeRepository.Get().ToList().Count;
    //        if (count == 0)
    //        {
    //            return StatusCode(404, new { status = HttpStatusCode.NotFound, result = employeeRepository.Get(), message = "Data Tidak Ditemukan." });
    //        }
    //        else
    //        {
    //            return StatusCode(200, new { status = HttpStatusCode.OK, result = employeeRepository.Get(), message = "Data Ditemukan." });
    //        }
    //    }

    //    [HttpGet("{NIK}")]
    //    public ActionResult GetNIK(string NIK)
    //    {
    //        if (employeeRepository.Get(NIK) == null)
    //        {
    //            return StatusCode(404, new { status = HttpStatusCode.NotFound, result = employeeRepository.Get(NIK), message = $"Data dengan NIK { NIK } tidak ditemukan. DbUpdateConcurrencyException or DbUpdateException" });
    //        }
    //        else
    //        {
    //            return StatusCode(200, new { status = HttpStatusCode.OK, result = employeeRepository.Get(NIK), message = "Data Berhasil Dihapus." });
    //        }
    //    }

    //    [HttpPut]
    //    public ActionResult Put(Employee employee)
    //    {
    //        try
    //        {
    //            employeeRepository.Update(employee);
    //        }
    //        catch (Exception ex)
    //        {
    //            if (ex is DbUpdateConcurrencyException || ex is DbUpdateException)
    //            {
    //                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result = employee, message = "Data Gagal Diubah. DbUpdateConcurrencyException or DbUpdateException" });
    //            }
    //        }
    //        return StatusCode(200, new { status = HttpStatusCode.OK, result = employee, message = "Data Berhasil Diubah." });
    //    }

    //    [HttpPost]
    //    public ActionResult Post(Employee employee)
    //    {
    //        try
    //        {
    //            var insert = employeeRepository.Insert(employee);
    //            switch(insert)
    //            {
    //                case 0:
    //                    return Ok(new { status = HttpStatusCode.OK, result = employee, message = "Insert Data Successfull" });
    //                    break;
    //                case 1:
    //                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Failed, NIK already exists!" });
    //                    break;
    //                case 2:
    //                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Failed, Email already exists!" });
    //                    break;
    //                case 3:
    //                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Failed, Phone already exists!" });
    //                    break;
    //            };

    //        }
    //        catch
    //        {
    //                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = employee, message = "Error Occured." });
    //        }
    //        return Ok();
    //    }

    //    [HttpDelete("{NIK}")] 
    //    public ActionResult Delete(string NIK)
    //    {
    //        try
    //        {
    //            var employee = employeeRepository.Get(NIK);
    //            employeeRepository.Delete(NIK);
    //            return StatusCode(200, new { status = HttpStatusCode.OK, result = employee, message = "Data Berhasil Dihapus." });
    //        }
    //        catch (Exception ex)
    //        {
    //            if (ex is ArgumentNullException)
    //            {
    //                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result = employeeRepository.Get(NIK), message = "Data Gagal Dihapus. ArgumentNullException" });
    //            }
    //        }
    //        return StatusCode(200);

    //    }
    //}
}
