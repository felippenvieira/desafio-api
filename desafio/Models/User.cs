using System;

namespace desafio_api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}