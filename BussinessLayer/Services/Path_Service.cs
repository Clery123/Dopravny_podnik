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
    public class Path_Service
    {
        public static void Update(Path pat)
        {
            PathTable.Update(pat);
        }
        public static void Insert(Path pat)
        {
            //Todo
            PathTable.Insert(pat);
        }
        public static int Del(int id)
        {
            return PathTable.Delete(id);
        }
        public static Collection<Path> getAll()
        {
            return PathTable.Select();
        }
        public static Path getByID(int id)
        {
            return PathTable.SelectID(id);
        }
    }
}
