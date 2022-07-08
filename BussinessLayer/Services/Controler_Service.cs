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
    public class Controler_Service
    {
        public static void Update(Controler con)
        {
            ControlerTable.Update(con);
        }
        public static void Insert(Controler dis)
        {
            //Todo
            ControlerTable.Insert(dis);
        }
        public static int Del(int id)
        {
            return ControlerTable.Delete(id);
        }
        public static Collection<Controler> getAll()
        {
            return ControlerTable.Select();
        }
        public static Controler getByID(int id)
        {
            return ControlerTable.SelectID(id);
        }
        public static Controler getVehicle(int id)
        {
            return ControlerTable.SelectID(id);
        }
    }
}
