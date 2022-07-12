using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAPI.Data.Models;

namespace WebAPI.Security
{
    [Serializable]
    public class Principal
    {
        public class InvalidJsonException : Exception {
            public InvalidJsonException() : base("Invalid json format for a principal") {}
        }
        public class InvalidClaimsException : Exception {
            public InvalidClaimsException() : base("Invalid claims format for a principal") {}
        }

        public int Id { get; set; }
        public int Role { get; set; }

        public static Principal Parse(string json) {
            var serializer = JsonSerializer.Create();
            var jsonReader = new JsonTextReader(new StringReader(json));

            var principal = serializer.Deserialize<Principal>(jsonReader);
            if (principal != null) {
                return principal;
            } else {
                throw new InvalidJsonException();
            }
        }
        public override string ToString()
        {
            using var textWriter = new StringWriter();
            var serializer = JsonSerializer.Create();
            
            serializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }
        public static Principal Parse(IEnumerable<Claim> claims) {
            var type = typeof(Principal);
            var result = new Principal();

            try {
                foreach (var claim in claims) {
                    var prop = type.GetProperty(claim.Type);
                    if (prop == null) continue;

                    var propType = prop.GetType();
                    if (propType == typeof(string)) {
                        prop.SetValue(result, claim.Value);
                    } else if (propType == typeof(int)) {
                        prop.SetValue(result, int.Parse(claim.Value));
                    } else if (propType == typeof(float)) {
                        prop.SetValue(result, float.Parse(claim.Value));
                    } else {
                        throw new InvalidClaimsException();
                    }
                }
            } catch (Exception) {
                throw new InvalidClaimsException();
            }

            return result;
        }
        public IEnumerable<Claim> ToClaims() {
            var properties = this.GetType().GetProperties();
            var claims = new List<Claim>();

            foreach (var prop in properties) {
                claims.Add(new Claim(prop.Name, prop.GetValue(this)!.ToString()!));
            }

            return claims;
        }
    }
}