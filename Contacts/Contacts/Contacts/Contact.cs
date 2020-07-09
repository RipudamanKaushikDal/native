using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts
{
    class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [MaxLength(10)]
        public string Phone { get; set; }
    }
}
