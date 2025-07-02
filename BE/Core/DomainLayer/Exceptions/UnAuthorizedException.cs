using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UnAuthorizedException(string? message = "Invalid Email Or Password") : Exception("Invalid Email Or Password")
    {
    }
}
