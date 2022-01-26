using System;
using System.Collections.Generic;
using API.Extantions;

namespace API.Entities
{
    public class AppUser
    {
                public int Id { get; set; }
                public string UserName { get; set; }

                public byte[] PasswordHash { get; set; }     
                public byte[] PasswordSalt { get; set; }  

                public DateTime BirtheDay { get; set; }

                public string KnowAs { get; set; }
                
                public DateTime Created { get; set; } = DateTime.Now;
                public DateTime LestActive { get; set; } = DateTime.Now;
                public string Gender { get; set; }
                public string Introdution { get; set; }
                  public string LookingFor { get; set; }
                   public string Interested { get; set; }
                
                public ICollection<Photo> Photos { get; set; }

                public int GetAge(){
                    return DataTimeExtantions.CalculateAge(BirtheDay);
                }
                
                
                
                
                
                
                

                
                
                   
                
    }
}