using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DAO;
using DTO.DTO;
namespace BussinessLayer.Services
{
    public class Employee_Service
    {
        public static void Bonus()
        {
            Collection<Driver> drivers = DriverTable.Select();
            Collection<Dispatcher> dispatchers = DispatcherTable.Select();
            Collection<Controler> controlers = ControlerTable.Select();

            foreach(Driver driver in drivers)
            {
                if(driver.Hours_worked >= 750)
                {
                    Employee emp = EmployeeTable.SelectID(driver.eID);
                    emp.Salary += 25;
                    EmployeeTable.Update(emp, driver.eID);
                }
            }
            foreach(Dispatcher dis in dispatchers)
            {
                if (dis.Hours_worked >= 750)
                {
                    Employee emp1 = EmployeeTable.SelectID(dis.eID);
                    emp1.Salary += 25;
                    EmployeeTable.Update(emp1, dis.eID);
                }
            }
            int average = 0;
            Controler last = controlers.Last();
            foreach (Controler controler in controlers)
            {
                average += controler.Number_of_fines;
                if (controler.Equals(last))
                {
                    average = average / controlers.Count();
                    foreach(Controler con in controlers)
                    {
                        if(con.Number_of_fines > average)
                        {
                            Employee emp2 = EmployeeTable.SelectID(con.eID);
                            emp2.Salary += 15;
                            EmployeeTable.Update(emp2, con.eID);
                        }
                    }
                }
            }

        }
        public static void Update(Employee emp, int id)
        {
            EmployeeTable.Update(emp, id);
            EmployeeTable.LogE("Employee", "Update");
        }
        public static void InsertD(Employee emp)
        {
            //Todo
            EmployeeTable.InsertD(emp);
            EmployeeTable.LogE("Employee-Driver", "Insert");
        }
        public static void InsertC(Employee emp)
        {
            //Todo
            EmployeeTable.InsertC(emp);
            EmployeeTable.LogE("Employee-Controler", "Insert");
        }
        public static void InsertDis(Employee emp)
        {
            //Todo
            EmployeeTable.InsertDis(emp);
            EmployeeTable.LogE("Employee-Dispatcher", "Insert");
        }
        public static int Del(int id)
        {
            return EmployeeTable.Delete(id);
        }
        public static Collection<Employee> getAll()
        {
            return EmployeeTable.Select();
        }
        public static Employee getByID(int id)
        {
            return EmployeeTable.SelectID(id);
        }
    }
}
