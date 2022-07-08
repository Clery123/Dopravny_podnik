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
    public class Schedule_Service
    {
        public static void Update(Schedule sch)
        {
            ScheduleTable.Update(sch);
        }
        public static void Insert(Schedule sch)
        {
            //Todo
            ScheduleTable.Insert(sch);
        }
        public static string Del(int id)
        {
            return ScheduleTable.Del(id);
        }
        public static Collection<Schedule> getAll()
        {
            return ScheduleTable.Select();
        }
        public static Schedule getByID(int id)
        {
            return ScheduleTable.SelectID(id);
        }
    }
}
