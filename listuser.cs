using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlsv_pj
{
    public class listuser
    {
        private static listuser instance;
        private List<user> listacuser;

        public List<user> Listacuser { get => listacuser; set => listacuser = value; }
        public static listuser Instance {
            get
            {
                if (instance == null) instance = new listuser();
                return instance;
            }
            set => instance = value; }

        private listuser()
        {
            listacuser = new List<user>();
            listacuser.Add(new user("admin", "123",true));
            listacuser.Add(new user("user", "123",false));
        }
    }
}
