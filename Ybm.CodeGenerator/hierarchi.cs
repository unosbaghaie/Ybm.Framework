using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class hierarchi
    {
        public enum EnumType
        {
            Drive = 0,
            Folder = 1,
            File = 2
        }
        public int Id { get; set; }
        public string ItemName { get; set; }
        public EnumType TypeId { get; set; }
        public ICollection<hierarchi> Children { get; set; }


        //public  void AddItem()
        //{
        //    hierarchi h = new hierarchi();
        //    return h.Children.Where(q => q.TypeId == EnumType.Drive).ToList();
        //}

        public ICollection<hierarchi> GetAllDrives()
        {
            hierarchi h = new hierarchi();
            return h.Children.Where(q => q.TypeId == EnumType.Drive).ToList();
        }

        hierarchi list = new hierarchi();
        public void GetAllFolders(ICollection<hierarchi> list)
        {
            if (!list.Any())
            {
                
                foreach (var item in (list.Where(q=>q.TypeId == EnumType.Folder)))
                {
                    Console.WriteLine(item.ItemName);
                    GetAllFolders(item.Children);
                }
            }
        }
    }
}
