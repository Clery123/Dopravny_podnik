using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.DTO;
using DataLayer.DAO;
using System.Collections.ObjectModel;

namespace BussinessLayer.Services
{
    public class Vehicle_Service
    {
        public static void Update(Vehicle veh, int id)
        {
            VehicleTable.Update(veh, id);
            VehicleTable.LogV("Vehicle", "Update");
        }
        public static void Insert(Vehicle veh)
        {
            VehicleTable.Insert(veh);
            VehicleTable.LogV("Vehicle", "Insert");
        }
        public static String Del(int id)
        {
            return VehicleTable.Del(id);
        }
        public static Collection<Vehicle> getAll()
        {
            return VehicleTable.Select();
        }
        public static Vehicle getVehicle(int id)
        {
            return VehicleTable.SelectID(id);
        }
        public static Collection<Vehicle> getFree()
        {
            Collection<Vehicle> vehicles = VehicleTable.Select();
            Collection<Vehicle> freeVehicles = new Collection<Vehicle>();
            foreach (Vehicle vh in vehicles)
            {
                if (vh.Condition != "DISCARDED" && vh.Condition != "IN USE")
                {
                    freeVehicles.Add(vh);
                }
            }
            return freeVehicles;
        }
        public static void Vehicle_Check()
        {
            Collection<Vehicle> veh = VehicleTable.Select();
            Collection<Schedule> sch = ScheduleTable.Select();
            foreach(Vehicle v in veh)
            {
                if (v.Year_of_manufacture > 15)
                {
                    v.Condition = "DISCARDED";
              
                }
                else {
                    foreach (Schedule s in sch)
                    {
                        if (v.Id == s.vID)
                        {
                            v.Condition = "IN USE";
                            break;
                        }
                        else
                        {
                            v.Condition = "FREE";
                            
                        }
                }
                }
            }
            foreach (Vehicle vh in veh)
            {
                VehicleTable.Update(vh,vh.Id);
            }
            //List<Vehicle> veh = new List<Vehicle>();
        }
    }
}
