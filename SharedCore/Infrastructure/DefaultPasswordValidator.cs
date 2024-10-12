using SharedOperations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedOperations.Infrastructure
{
    public class DefaultPasswordValidator : IPasswordValidator
    {

        public bool IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false; // La contraseña no puede ser vacía o solo espacios en blanco
            }

            // Requisito de longitud mínima
            if (password.Length < 8)
            {
                return false;
            }

            // Debe contener al menos una letra mayúscula
            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                return false;
            }

            // Debe contener al menos una letra minúscula
            if (!Regex.IsMatch(password, "[a-z]"))
            {
                return false;
            }

            // Debe contener al menos un número
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }

            // Debe contener al menos un carácter especial
            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                return false;
            }

            // Si pasa todos los requisitos, la contraseña es válida
            return true;
        }

    }
}
