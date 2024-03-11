using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlsv_pj
{
    public class user
    {
        private string username;
        private string password;
        private bool actype;
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Actype { get => actype; set => actype = value; }

        public user(string un, string pw,bool at) {
            this.username = un;
            this.password = pw;
            this.actype = at;
        }
    }
}
