using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class UserAccount : BaseEntities
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }
    }
}
