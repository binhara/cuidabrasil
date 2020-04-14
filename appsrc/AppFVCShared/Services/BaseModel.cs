using System;

namespace AppFVCShared.Services
{
    public interface IBaseModel
    {
        string Id { get; set; }
    }
    //[Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public abstract class BaseModel : IBaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
