using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DataLayer.DAO;
using DTO.DTO;
namespace BussinessLayer.Services
{
    public class Driver_Service
    {
        //USE CASE
        public static double Daily(int id)
        {
            double distance = 0;
            Collection<Schedule> schedules = ScheduleTable.Select();
            foreach(Schedule sch in schedules)
            {
                if(sch.dID == id)
                {
                    
                    distance += PathTable.SelectID(sch.pID).Distance;
                }
            }
            return distance;
        }
        public static Collection<Vehicle> Added_vehicles(int idckoV)
        {
            return DriverTable.Added_vehicles(idckoV);
        }
        public static void Update(Driver driver)
        {
            DriverTable.Update(driver);
        }
        public static void Insert(Driver drv)
        {
            //Todo
            DriverTable.Insert(drv);
        }
        public static String Del(int id)
        {
            return DriverTable.Del(id);
        }
        public static Collection<Driver> getAll()
        {
            return DriverTable.Select();
        }
        public static Driver getByID(int id)
        {
            return DriverTable.SelectID(id);
        }
    }
}
