using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace API.Repository
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {

        }

        //public override int Insert(Employee employee)
        //{
        //    if (myContext.Employees.ToList().Count == 0)
        //    {
        //        employee.NIK = DateTime.Now.Year.ToString() + "001";
        //        myContext.Employees.Add(employee);
        //        var result = myContext.SaveChanges();
        //        return 0;
        //    }
        //    else
        //    {
        //        int lastNIK = Convert.ToInt32(myContext.Employees.ToList().LastOrDefault().NIK) % DateTime.Now.Year + 1;
        //        string incNIK = DateTime.Now.ToString("yyyy") + lastNIK.ToString("D3");
        //        var cekNIK = myContext.Employees.Any(e => e.NIK == incNIK);
        //        var cekEmail = myContext.Employees.Any(e => e.Email == employee.Email);
        //        var cekPhone = myContext.Employees.Any(e => e.Phone == employee.Phone);
        //        if (cekNIK)
        //        {
        //            return 1;
        //        }
        //        else if (cekEmail)
        //        {
        //            return 2;
        //        }
        //        else if (cekPhone)
        //        {
        //            return 3;
        //        }
        //        else
        //        {
        //            employee.NIK = incNIK;
        //            employee.BirthDate = DateTime.ParseExact(Convert.ToString(employee.BirthDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //            myContext.Employees.Add(employee);
        //            var result = myContext.SaveChanges();
        //            return 0;
        //        }
        //    }
        //}
        //public int Delete(string NIK)
        //{
        //    var entity = context.Employees.Find(NIK);
        //    context.Remove(entity);
        //    var result = context.SaveChanges();
        //    return result;
        //    //throw new System.NotImplementedException();
        //}

        //public IEnumerable<Employee> Get()
        //{
        //    return context.Employees.ToList();
        //    //throw new System.NotImplementedException();
        //}

        //public Employee Get(string NIK)
        //{
        //    return context.Employees.Find(NIK);

        //    ////Menampilkan data pertama
        //    //return context.Employees.First(e => e.FirstName == NIK);

        //    ////Error ketika ada lebih dari satu data
        //    //return context.Employees.Single(e => e.FirstName == NIK);

        //    ////Return null ketika ada lebih dari satu data
        //    //return context.Employees.SingleOrDefault(e => e.FirstName == NIK);

        //    ////Return null ketika tidak ada data
        //    //return context.Employees.FirstOrDefault(e => e.FirstName == NIK);
        //}



        //public int Update(Employee employee)
        //{
        //    context.Entry(employee).State = EntityState.Modified;
        //    var result = context.SaveChanges();
        //    return result;
        //    //throw new System.NotImplementedException();
        //}

        //private readonly MyContext context;
        //public EmployeeRepository(MyContext context)
        //{
        //    this.context = context;
        //}
    }
}
