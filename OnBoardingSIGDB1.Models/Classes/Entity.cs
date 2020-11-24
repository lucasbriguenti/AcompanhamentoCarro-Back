using OnBoardingSIGDB1.Models.Classes;
using System.Runtime.Serialization;

namespace OnboardingSIGDB1.Models.Classes
{
    [DataContract]
    public abstract class Entity : EntityValidator
    {
        [DataMember]
        public int Id { get; set; }
	}
}
