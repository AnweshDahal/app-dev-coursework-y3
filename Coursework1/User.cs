using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class User
    {
        /// <summary>
        /// The User's Login username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The User's Login pasword
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// the User's admin status
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
