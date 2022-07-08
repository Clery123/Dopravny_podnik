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
    public class Dispatcher_Service
    {
        public static void Update(Dispatcher dis)
        {
            DispatcherTable.Update(dis);
        }
        public static void Insert(Dispatcher dis)
        {
             DispatcherTable.Insert(dis);
        }
        public static int Del(int id)
        {
            return DispatcherTable.Delete(id);
        }
        public static Collection<Dispatcher> getAll()
        {
            return DispatcherTable.Select();
        }
        public static Dispatcher getByID(int id)
        {
            return DispatcherTable.SelectID(id);
        }
        public static Dispatcher getVehicle(int id)
        {
            return DispatcherTable.SelectID(id);
        }
    }
}
